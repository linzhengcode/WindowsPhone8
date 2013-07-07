// PhoneDLL1.cpp: 定义 DLL 应用程序的导出函数。
//

#include "pch.h"
#include "PhoneDLL1.h"

#include <stdexcept>

using namespace std;

namespace MathFuncs2
{
  double MyMathFuncs2::Add(double a, double b)
  {
      return a + b;
  }
}