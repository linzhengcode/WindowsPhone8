// WindowsPhoneRuntimeComponent1.cpp
#include "pch.h"
#include "WindowsPhoneRuntimeComponent1.h"
#include "PhoneLib1.h"
#include "PhoneDLL1.h"

using namespace WindowsPhoneRuntimeComponent1;
using namespace Platform;

WindowsPhoneRuntimeComponent::WindowsPhoneRuntimeComponent()
{
}

int WindowsPhoneRuntimeComponent::Add(int a, int b)
{
	//return MathFuncs::MyMathFuncs::Add(a,b);//静态库的调用
	return MathFuncs2::MyMathFuncs2::Add(a,b);//动态库的调用
}