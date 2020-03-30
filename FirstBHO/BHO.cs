using SHDocVw;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System;
using System.Security.Permissions;
using Microsoft.VisualStudio.OLE.Interop;
using System.Diagnostics;

namespace FirstBHO
{
    [
        SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode),
        ComVisible(true),
        Guid("09b2f885-9bd8-478d-beb7-015e362dfb6d"),
        ClassInterface(ClassInterfaceType.None)
    ]
    public class BHO : IObjectWithSite, IOleCommandTarget
    {
        WebBrowser webBrowser;

        public int SetSite([MarshalAs(UnmanagedType.IUnknown)] object site)
        {
#if DEBUG
            Debugger.Launch();
#endif
            try
            {
                if (site != null)
                {
                    webBrowser = (SHDocVw.WebBrowser)site;
                    webBrowser.BeforeNavigate2 += OnBeforeNavigate2;
                }
                else
                {
                    webBrowser.BeforeNavigate2 -= OnBeforeNavigate2;
                    webBrowser = null;
                }
            }
            catch (Exception ex)
            {

            }

            return 0;
        }

        public int GetSite(ref Guid guid, out IntPtr ppvSite)
        {
            IntPtr punk = Marshal.GetIUnknownForObject(webBrowser);
            int hr = Marshal.QueryInterface(punk, ref guid, out ppvSite);
            Marshal.Release(punk);
            return hr;
        }

        public void OnBeforeNavigate2(object pDisp, ref object URL, ref object Flags,
       ref object TargetFrameName, ref object PostData, ref object Headers, ref bool Cancel)
        {
            if (URL.ToString().Contains("facebook.com") || URL.ToString().Contains("youtube.com"))
            {
                try
                {
                    Cancel = true;
                    URL = "www.google.com";

                    webBrowser.Navigate2(URL, Flags, TargetFrameName, PostData, Headers);
                }
                catch (Exception)
                {
                }
            }
        }

        [ComRegisterFunction]
        public static void RegisterBHO(Type type)
        {
            RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Browser Helper Objects");
            RegistryKey guidKey = registryKey.CreateSubKey(type.GUID.ToString("B"));

            registryKey.Close();
            guidKey.Close();
        }

        [ComUnregisterFunction]
        public static void UnregisterBHO(Type type)
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Browser Helper Objects", true);

            string guid = type.GUID.ToString("B");

            if (registryKey != null)
            {
                registryKey.DeleteSubKey(guid, false);
            }
        }

        public int Exec(ref Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
        {
            return 0;
        }

        public int QueryStatus(ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText)
        {
            new MainForm().ShowDialog();
            return 0;
        }
    }
}
