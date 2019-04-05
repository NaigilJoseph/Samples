// TraditionalAPI.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "TraditionalAPI.h"

#include <math.h>
#include <tchar.h>


TRADITIONALAPI_API void __cdecl TA_IncrementCounter()
{
	TA_IncrementCounterStatic();
}

//	A slightly more complex function, find the square root of a double.
TRADITIONALAPI_API double __cdecl TA_CalculateSquareRoot(double dValue)
{
	return TA_CalculateSquareRootStatic(dValue);
}

//	A function that would require array marshalling, find the dot product
//	of two three tuples.
TRADITIONALAPI_API double __cdecl TA_DotProduct(double arThreeTuple1[], double arThreeTuple2[])
{
	return TA_DotProductStatic(arThreeTuple1, arThreeTuple2);
}

TRADITIONALAPI_API char* TestStructInStruct(MYPERSON2* pPerson2)
{
	return TestStructInStructStatic(pPerson2);
}


TRADITIONALAPI_API  void DeleteObjectAPI(void* pData)
{
	return DeleteObjectStatic(pData);
}