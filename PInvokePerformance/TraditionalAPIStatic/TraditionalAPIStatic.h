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

//	A function that would require array marshalling, find the dot product
//	of two three tuples.
double TA_DotProductStatic(double arThreeTuple1[], double arThreeTuple2[]);

char* TestStructInStructStatic(MYPERSON2* pPerson2);
void DeleteObjectStatic(void* pData);
