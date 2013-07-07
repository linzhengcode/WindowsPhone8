#pragma once
 #include "ppltasks.h" 
using namespace concurrency;
using namespace Windows::Foundation;
using namespace Platform;

namespace WindowsPhoneRuntimeComponent1
{
    public ref class WindowsPhoneRuntimeComponent sealed
    {
    public:
        WindowsPhoneRuntimeComponent();
		IAsyncAction^ AddContactAsync(String^ name);
    };
}