// TraditionalAPIStatic.cpp : Defines the functions for the static library.
//

#include "pch.h"
#include "framework.h"
#include "TraditionalAPIStatic.h"
#include <string.h>


//	A global counter.
unsigned int g_uCounter = 0;
double g_dResult = 0;

void TA_IncrementCounterStatic()
{
	g_uCounter++;
}

//	A function that would require array marshalling, find the dot product
//	of two three tuples.
double TA_DotProductStatic(double arThreeTuple1[], double arThreeTuple2[])
{
	return arThreeTuple1[0] * arThreeTuple2[0] + arThreeTuple1[1] * arThreeTuple2[1] + arThreeTuple1[2] * arThreeTuple2[2];
}

char* TestStructInStructStatic(MYPERSON2* pPerson2)
{

	if (pPerson2 == nullptr || pPerson2->person.first == nullptr)
	{
		return nullptr;
	}

	size_t firstLen = strlen(pPerson2->person.first);
	size_t secondLen = strlen(pPerson2->person.last);

	char* fullName = new char[firstLen + secondLen + 10];

	memset(fullName, 0, firstLen + secondLen + 10);

	strcpy_s(fullName, firstLen + 1, pPerson2->person.first);
	strcpy_s(fullName + firstLen, secondLen + 1, pPerson2->person.last);
	return fullName;
}

void DeleteObjectStatic(void* pData)
{
	delete pData;
}
