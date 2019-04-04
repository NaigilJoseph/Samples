#pragma once


typedef struct _MYPERSON
{
	char* first;
	char* last;
} MYPERSON, * LP_MYPERSON;

typedef struct _MYPERSON2
{
	MYPERSON person;
	int age;
} MYPERSON2, * LP_MYPERSON2;


//	The simplest function, increment a counter.

void TA_IncrementCounterStatic();

//	A slightly more complex function, find the square root of a double.
double TA_CalculateSquareRootStatic(double dValue);

//	A function that would require array marshalling, find the dot product
//	of two three tuples.
double TA_DotProductStatic(double arThreeTuple1[], double arThreeTuple2[]);

//	Run each test x times.
unsigned int TA_Test1Static(double nTestCount);
double TA_Test2Static(double nTestCount);
double TA_Test3Static(double nTestCount);
char* TestStructInStruct(MYPERSON2* pPerson2);
void DeleteString(char* personName);
//void TestStructInStruct3(MYPERSON3 person3);
//void TestArrayInStruct(MYARRAYSTRUCT* pStruct);
