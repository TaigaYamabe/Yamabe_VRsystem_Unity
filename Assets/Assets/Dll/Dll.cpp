#include "stdafx.h"
#include "pch.h"
#include <float.h>
#include <iostream>

// C#から呼ばれる関数の定義  
DllExport void TestFloat(const float value);

void TestInt(const float value)
{
        std::cout << value << std::endl;
}