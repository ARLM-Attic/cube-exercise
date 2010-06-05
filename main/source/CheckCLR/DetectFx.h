#pragma once

// Constants that represent registry key names and value names
// to use for detection
extern const TCHAR *g_szNetfx10RegKeyName; // = _T("Software\\Microsoft\\.NETFramework\\Policy\\v1.0");
extern const TCHAR *g_szNetfx10RegKeyValue; // = _T("3705");
extern const TCHAR *g_szNetfx10SPxMSIRegKeyName; // = _T("Software\\Microsoft\\Active Setup\\Installed Components\\{78705f0d-e8db-4b2d-8193-982bdda15ecd}");
extern const TCHAR *g_szNetfx10SPxOCMRegKeyName; // = _T("Software\\Microsoft\\Active Setup\\Installed Components\\{FDC11A6F-17D1-48f9-9EA3-9051954BAA24}");
extern const TCHAR *g_szNetfx11RegKeyName; // = _T("Software\\Microsoft\\NET Framework Setup\\NDP\\v1.1.4322");
extern const TCHAR *g_szNetfx20RegKeyName; // = _T("Software\\Microsoft\\NET Framework Setup\\NDP\\v2.0.50727");
extern const TCHAR *g_szNetfx30RegKeyName; // = _T("Software\\Microsoft\\NET Framework Setup\\NDP\\v3.0\\Setup");
extern const TCHAR *g_szNetfx30SpRegKeyName; // = _T("Software\\Microsoft\\NET Framework Setup\\NDP\\v3.0");
extern const TCHAR *g_szNetfx30RegValueName; // = _T("InstallSuccess");
extern const TCHAR *g_szNetfx35RegKeyName; // = _T("Software\\Microsoft\\NET Framework Setup\\NDP\\v3.5");
extern const TCHAR *g_szNetfx40ClientRegKeyName; // = _T("Software\\Microsoft\\NET Framework Setup\\NDP\\v4\\Client");
extern const TCHAR *g_szNetfx40FullRegKeyName; // = _T("Software\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full");
extern const TCHAR *g_szNetfx40SPxRegValueName; // = _T("Servicing");
extern const TCHAR *g_szNetfxStandardRegValueName; // = _T("Install");
extern const TCHAR *g_szNetfxStandardSPxRegValueName; // = _T("SP");
extern const TCHAR *g_szNetfxStandardVersionRegValueName; // = _T("Version");

// Version information for final release of .NET Framework 3.0
extern const int g_iNetfx30VersionMajor; // = 3;
extern const int g_iNetfx30VersionMinor; // = 0;
extern const int g_iNetfx30VersionBuild; // = 4506;
extern const int g_iNetfx30VersionRevision; // = 26;

// Version information for final release of .NET Framework 3.5
extern const int g_iNetfx35VersionMajor; // = 3;
extern const int g_iNetfx35VersionMinor; // = 5;
extern const int g_iNetfx35VersionBuild; // = 21022;
extern const int g_iNetfx35VersionRevision; // = 8;

// Version information for final release of .NET Framework 4.0
extern const int g_iNetfx40VersionMajor; // = 4;
extern const int g_iNetfx40VersionMinor; // = 0;
extern const int g_iNetfx40VersionBuild; // = 30319;
extern const int g_iNetfx40VersionRevision; // = 1;

// Constants for known .NET Framework versions used with the GetRequestedRuntimeInfo API
extern const TCHAR *g_szNetfx10VersionString; // = _T("v1.0.3705");
extern const TCHAR *g_szNetfx11VersionString; // = _T("v1.1.4322");
extern const TCHAR *g_szNetfx20VersionString; // = _T("v2.0.50727");
extern const TCHAR *g_szNetfx40VersionString; // = _T("v4.0.30319");

// Function prototypes
bool CheckNetfxBuildNumber(const TCHAR*, const TCHAR*, const int, const int, const int, const int);
bool CheckNetfxVersionUsingMscoree(const TCHAR*);
int GetNetfx10SPLevel();
int GetNetfxSPLevel(const TCHAR*, const TCHAR*);
DWORD GetProcessorArchitectureFlag();
bool IsCurrentOSTabletMedCenter();
bool IsNetfx10Installed();
bool IsNetfx11Installed();
bool IsNetfx20Installed();
bool IsNetfx30Installed();
bool IsNetfx35Installed();
bool IsNetfx40ClientInstalled();
bool IsNetfx40FullInstalled();
bool RegistryGetValue(HKEY, const TCHAR*, const TCHAR*, DWORD, _Out_cap_(dwSize)LPBYTE, DWORD dwSize);

