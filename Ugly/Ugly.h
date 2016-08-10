// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the UGLY_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// UGLY_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef UGLY_EXPORTS
#define UGLY_API __declspec(dllexport)
#else
#define UGLY_API __declspec(dllimport)
#endif

// This class is exported from the Ugly.dll
class UGLY_API CUgly {
public:
	CUgly(void);
};

extern UGLY_API int nUgly;

extern "C" {
	UGLY_API int fnUgly(void);
}
