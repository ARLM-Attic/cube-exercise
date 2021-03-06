#include <windows.h>
#include <tchar.h>
#include "DetectFx.h"
#include "Version.h"

// For waining C4100: 'variable name': unreferenced formal parameter
#undef UNREFERENCED
#define UNREFERENCED(x)     (x)

int APIENTRY wWinMain(HINSTANCE hInstance,
                      HINSTANCE hPrevInstance,
                      LPWSTR    lpCmdLine,
                      int       nCmdShow)
{
    UNREFERENCED(hInstance);
    UNREFERENCED(hPrevInstance);
    UNREFERENCED(lpCmdLine);
    UNREFERENCED(nCmdShow);

    int iNetfx40ClientSPLevel = -1;
    // int iNetfx40FullSPLevel = -1;
    TCHAR szMessage[MAX_PATH];
    TCHAR szOutputString[MAX_PATH*20];
    ::ZeroMemory(szMessage, sizeof(szMessage));
    ::ZeroMemory(szOutputString, sizeof(szOutputString));

    // Determine whether or not the .NET Framework 4.0 Client Profile is installed

    bool bNetfx40ClientInstalled = (IsNetfx40ClientInstalled());
    // bool bNetfx40FullInstalled = (IsNetfx40FullInstalled());

    // If .NET Framework 4 Client is installed, get the
    // service pack level
    if (bNetfx40ClientInstalled)
    {
        iNetfx40ClientSPLevel = GetNetfxSPLevel(g_szNetfx40ClientRegKeyName, g_szNetfx40SPxRegValueName);

        if (iNetfx40ClientSPLevel > 0)
            _stprintf_s(szMessage, MAX_PATH, _T("\n\n.NET Framework 4 Client Profile Service Pack %i已安装，您可以正常使用CubeExercise。"), iNetfx40ClientSPLevel);
        else
            _stprintf_s(szMessage, MAX_PATH, _T("\n\n.NET Framework 4 Client Profile已安装，您可以正常使用CubeExercise。"));

        _tcscat_s(szOutputString, szMessage);
    }
    else
    {
        _tcscat_s(szOutputString, _T("\n\n.NET Framework 4 Client Profile没有安装，您将无法使用CubeExercise。请按照Readme.mht中的说明安装.NET Framework 4.0 Client Profile。"));
    }

    MessageBoxW(NULL, szOutputString, L"CubeExercise运行条件检查", MB_OK | MB_ICONINFORMATION);

    return 0;
}


//int APIENTRY wWinMain(HINSTANCE hInstance,
//                      HINSTANCE hPrevInstance,
//                      LPWSTR    lpCmdLine,
//                      int       nCmdShow)
//{
//    int iNetfx10SPLevel = -1;
//    int iNetfx11SPLevel = -1;
//    int iNetfx20SPLevel = -1;
//    int iNetfx30SPLevel = -1;
//    int iNetfx35SPLevel = -1;
//    int iNetfx40ClientSPLevel = -1;
//    int iNetfx40FullSPLevel = -1;
//    TCHAR szMessage[MAX_PATH];
//    TCHAR szOutputString[MAX_PATH*20];
//
//    // Determine whether or not the .NET Framework
//    // 1.0, 1.1, 2.0, 3.0, 3.5 or 4 are installed
//    bool bNetfx10Installed = (IsNetfx10Installed() && CheckNetfxVersionUsingMscoree(g_szNetfx10VersionString));
//    bool bNetfx11Installed = (IsNetfx11Installed() && CheckNetfxVersionUsingMscoree(g_szNetfx11VersionString));
//    bool bNetfx20Installed = (IsNetfx20Installed() && CheckNetfxVersionUsingMscoree(g_szNetfx20VersionString));
//
//    // The .NET Framework 3.0 is an add-in that installs
//    // on top of the .NET Framework 2.0.  For this version
//    // check, validate that both 2.0 and 3.0 are installed.
//    bool bNetfx30Installed = (IsNetfx20Installed() && IsNetfx30Installed() && CheckNetfxVersionUsingMscoree(g_szNetfx20VersionString));
//
//    // The .NET Framework 3.5 is an add-in that installs
//    // on top of the .NET Framework 2.0 and 3.0.  For this version
//    // check, validate that 2.0, 3.0 and 3.5 are installed.
//    bool bNetfx35Installed = (IsNetfx20Installed() && IsNetfx30Installed() && IsNetfx35Installed() && CheckNetfxVersionUsingMscoree(g_szNetfx20VersionString));
//
//    bool bNetfx40ClientInstalled = (IsNetfx40ClientInstalled());
//    bool bNetfx40FullInstalled = (IsNetfx40FullInstalled());
//
//    // If .NET Framework 1.0 is installed, get the
//    // service pack level
//    if (bNetfx10Installed)
//    {
//        iNetfx10SPLevel = GetNetfx10SPLevel();
//
//        if (iNetfx10SPLevel > 0)
//            _stprintf_s(szMessage, MAX_PATH, _T(".NET Framework 1.0 service pack %i is installed."), iNetfx10SPLevel);
//        else
//            _stprintf_s(szMessage, MAX_PATH, _T(".NET Framework 1.0 is installed with no service packs."));
//
//        _tcscpy_s(szOutputString, szMessage);
//    }
//    else
//    {
//        _tcscpy_s(szOutputString, _T(".NET Framework 1.0 is not installed."));
//    }
//
//    // If .NET Framework 1.1 is installed, get the
//    // service pack level
//    if (bNetfx11Installed)
//    {
//        iNetfx11SPLevel = GetNetfxSPLevel(g_szNetfx11RegKeyName, g_szNetfxStandardSPxRegValueName);
//
//        if (iNetfx11SPLevel > 0)
//            _stprintf_s(szMessage, MAX_PATH, _T("\n\n.NET Framework 1.1 service pack %i is installed."), iNetfx11SPLevel);
//        else
//            _stprintf_s(szMessage, MAX_PATH, _T("\n\n.NET Framework 1.1 is installed with no service packs."));
//
//        _tcscat_s(szOutputString, szMessage);
//    }
//    else
//    {
//        _tcscat_s(szOutputString, _T("\n\n.NET Framework 1.1 is not installed."));
//    }
//
//    // If .NET Framework 2.0 is installed, get the
//    // service pack level
//    if (bNetfx20Installed)
//    {
//        iNetfx20SPLevel = GetNetfxSPLevel(g_szNetfx20RegKeyName, g_szNetfxStandardSPxRegValueName);
//
//        if (iNetfx20SPLevel > 0)
//            _stprintf_s(szMessage, MAX_PATH, _T("\n\n.NET Framework 2.0 service pack %i is installed."), iNetfx20SPLevel);
//        else
//            _stprintf_s(szMessage, MAX_PATH, _T("\n\n.NET Framework 2.0 is installed with no service packs."));
//
//        _tcscat_s(szOutputString, szMessage);
//    }
//    else
//    {
//        _tcscat_s(szOutputString, _T("\n\n.NET Framework 2.0 is not installed."));
//    }
//
//    // If .NET Framework 3.0 is installed, get the
//    // service pack level
//    if (bNetfx30Installed)
//    {
//        iNetfx30SPLevel = GetNetfxSPLevel(g_szNetfx30SpRegKeyName, g_szNetfxStandardSPxRegValueName);
//
//        if (iNetfx30SPLevel > 0)
//            _stprintf_s(szMessage, MAX_PATH, _T("\n\n.NET Framework 3.0 service pack %i is installed."), iNetfx30SPLevel);
//        else
//            _stprintf_s(szMessage, MAX_PATH, _T("\n\n.NET Framework 3.0 is installed with no service packs."));
//
//        _tcscat_s(szOutputString, szMessage);
//    }
//    else
//    {
//        _tcscat_s(szOutputString, _T("\n\n.NET Framework 3.0 is not installed."));
//    }
//
//    // If .NET Framework 3.5 is installed, get the
//    // service pack level
//    if (bNetfx35Installed)
//    {
//        iNetfx35SPLevel = GetNetfxSPLevel(g_szNetfx35RegKeyName, g_szNetfxStandardSPxRegValueName);
//
//        if (iNetfx35SPLevel > 0)
//            _stprintf_s(szMessage, MAX_PATH, _T("\n\n.NET Framework 3.5 service pack %i is installed."), iNetfx35SPLevel);
//        else
//            _stprintf_s(szMessage, MAX_PATH, _T("\n\n.NET Framework 3.5 is installed with no service packs."));
//
//        _tcscat_s(szOutputString, szMessage);
//    }
//    else
//    {
//        _tcscat_s(szOutputString, _T("\n\n.NET Framework 3.5 is not installed."));
//    }
//
//    // If .NET Framework 4 Client is installed, get the
//    // service pack level
//    if (bNetfx40ClientInstalled)
//    {
//        iNetfx40ClientSPLevel = GetNetfxSPLevel(g_szNetfx40ClientRegKeyName, g_szNetfx40SPxRegValueName);
//
//        if (iNetfx40ClientSPLevel > 0)
//            _stprintf_s(szMessage, MAX_PATH, _T("\n\n.NET Framework 4 client service pack %i is installed."), iNetfx40ClientSPLevel);
//        else
//            _stprintf_s(szMessage, MAX_PATH, _T("\n\n.NET Framework 4 client is installed with no service packs."));
//
//        _tcscat_s(szOutputString, szMessage);
//    }
//    else
//    {
//        _tcscat_s(szOutputString, _T("\n\n.NET Framework 4 client is not installed."));
//    }
//
//    // If .NET Framework 4 Full is installed, get the
//    // service pack level
//    if (bNetfx40FullInstalled)
//    {
//        iNetfx40FullSPLevel = GetNetfxSPLevel(g_szNetfx40FullRegKeyName, g_szNetfx40SPxRegValueName);
//
//        if (iNetfx40FullSPLevel > 0)
//            _stprintf_s(szMessage, MAX_PATH, _T("\n\n.NET Framework 4 full service pack %i is installed."), iNetfx40FullSPLevel);
//        else
//            _stprintf_s(szMessage, MAX_PATH, _T("\n\n.NET Framework 4 full is installed with no service packs."));
//
//        _tcscat_s(szOutputString, szMessage);
//    }
//    else
//    {
//        _tcscat_s(szOutputString, _T("\n\n.NET Framework 4 full is not installed."));
//    }
//
//    MessageBox(NULL, szOutputString, _T(".NET Framework Install Info"), MB_OK | MB_ICONINFORMATION);
//
//    return 0;
//}
