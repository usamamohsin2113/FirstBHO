using Microsoft.VisualStudio.OLE.Interop;
using System;
using System.Runtime.InteropServices;

namespace FirstBHO
{
    [
        ComImport(),
        ComVisible(true),
        Guid("B722BCCB-4E68-101B-A2BC-00AA00404770"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)
    ]

    interface IOleCommandTarget
    {
        int Exec(ref Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut);

        int QueryStatus(ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText);
    }
}
