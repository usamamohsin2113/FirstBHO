using System;
using System.Runtime.InteropServices;

namespace FirstBHO
{
    [
        ComImport(),
        ComVisible(true),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
        Guid("FC4801A3-2BA9-11CF-A229-00AA003D7352")
    ]
    interface IObjectWithSite
    {
        [PreserveSig]
        int SetSite([In, MarshalAs(UnmanagedType.IUnknown)]object site);

        [PreserveSig]
        int GetSite(ref Guid guid, out IntPtr ppvSite);
    }
}
