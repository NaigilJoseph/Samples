// TraditionalAPIStatic.cpp : Defines the functions for the static library.
//

#include "pch.h"
#include "framework.h"
#include "TraditionalAPIStatic.h"
#include <math.h>
#include <string.h>


//	A global counter.
unsigned int g_uCounter = 0;
double g_dResult = 0;

void TA_IncrementCounterStatic()
{
	g_uCounter++;
}

//	A slightly more complex function, find the square root of a double.
double TA_CalculateSquareRootStatic(double dValue)
{
	return ::sqrt(dValue);
}

//	A function that would require array marshalling, find the dot product
//	of two three tuples.
double TA_DotProductStatic(double arThreeTuple1[], double arThreeTuple2[])
{
	return arThreeTuple1[0] * arThreeTuple2[0] + arThreeTuple1[1] * arThreeTuple2[1] + arThreeTuple1[2] * arThreeTuple2[2];
}

//	Run a test x times.
unsigned int TA_Test1Static(double nTestCount)
{
	for (double i = 1; i <= nTestCount; i++)
		TA_IncrementCounterStatic();

	return g_uCounter;
}

//	Run a test x times.
double TA_Test2Static(double nTestCount)
{
	g_dResult = 0;
	for (double i = 1; i <= nTestCount; i++)
		g_dResult += TA_CalculateSquareRootStatic(i);

	return g_dResult;

}

//	Run a test x times.
double TA_Test3Static(double nTestCount)
{
	double arThreeTuple1[3];
	double arThreeTuple2[3];
	g_dResult = 0;
	for (double i = 1; i <= nTestCount; i++)
	{
		arThreeTuple1[0] = i;
		arThreeTuple1[1] = i;
		arThreeTuple1[2] = i;

		arThreeTuple2[0] = i;
		arThreeTuple2[1] = i;
		arThreeTuple2[2] = i;

		g_dResult += TA_DotProductStatic(arThreeTuple1, arThreeTuple2);
	}
	return g_dResult;
}

char* TestStructInStruct(MYPERSON2* pPerson2)
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

void DeleteString(char* personName)
{
	delete personName;
}
