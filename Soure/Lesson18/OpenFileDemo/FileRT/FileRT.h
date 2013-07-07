#pragma once
using namespace Platform;
namespace FileRT
{
    public ref class WindowsPhoneRuntimeComponent sealed
    {
    public:
        WindowsPhoneRuntimeComponent();
		static String^ GetFiles();
    };
}