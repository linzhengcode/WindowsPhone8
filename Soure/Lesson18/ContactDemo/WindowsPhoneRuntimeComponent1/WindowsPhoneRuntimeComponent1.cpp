// WindowsPhoneRuntimeComponent1.cpp
#include "pch.h"
#include "WindowsPhoneRuntimeComponent1.h"

using namespace WindowsPhoneRuntimeComponent1;
using namespace Platform;
using namespace Windows::Phone::PersonalInformation;

WindowsPhoneRuntimeComponent::WindowsPhoneRuntimeComponent()
{
}


IAsyncAction^ WindowsPhoneRuntimeComponent::AddContactAsync(String^ name)
{
	return create_async([name]()
	{
		return create_task(ContactStore::CreateOrOpenAsync()).then([name](ContactStore^ contactStore)
		{
			StoredContact^ storedContact=ref new StoredContact(contactStore);
			storedContact->DisplayName=name;
			return storedContact->SaveAsync();
		});
	});
}
