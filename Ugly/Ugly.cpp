// Ugly.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "Ugly.h"


// This is an example of an exported variable
UGLY_API int nUgly=0;

// This is an example of an exported function.
UGLY_API int fnUgly(void)
{
    return 42;
}

// This is the constructor of a class that has been exported.
// see Ugly.h for the class definition
CUgly::CUgly()
{
    return;
}
