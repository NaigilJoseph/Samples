// ManagedInterface.h

#pragma once

#include "..\TraditionalAPIStatic\TraditionalAPIStatic.h"

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace SharedData;

namespace ManagedInterface {

	public ref class ManagedInterface
	{
	public:

		void IncrementCounter()
		{
			//	Call the unmanaged function.
			::TA_IncrementCounterStatic();
		}

		double DotProduct(array<double>^ threeTuple1, array<double>^ threeTuple2)
		{
			//	Pin the arrays.
			pin_ptr<double> p1(&threeTuple1[0]);
			pin_ptr<double> p2(&threeTuple2[0]);
			
			//	Call the unmanaged function.
			return TA_DotProductStatic(p1, p2);
		}

		System::String^ TestStructInStruct(MyPerson2^ pPerson2)
		{
			// Allocate a buffer for serialization, pointer to NULL otherwise
			IntPtr ptr = Marshal::AllocCoTaskMem(Marshal::SizeOf(pPerson2));

			// Serialize the managed object to "static" memory (not managed by the GC)
			Marshal::StructureToPtr(pPerson2, ptr, false);

		    // Call the unmanaged function.
			char* pString = TestStructInStructStatic(reinterpret_cast<MYPERSON2*>(ptr.ToPointer()));
			String^ stringValue = gcnew String(pString);
			DeleteObjectStatic(pString);

			Marshal::FreeCoTaskMem(ptr);
			return stringValue;
		}
	};
}
