// FileRT.cpp
#include "pch.h"
#include "FileRT.h"

using namespace FileRT;
using namespace Platform;

#include <string>
#include <fstream>

#include <shellapi.h>


using namespace std;


WindowsPhoneRuntimeComponent::WindowsPhoneRuntimeComponent()
{
}

String^ WindowsPhoneRuntimeComponent::GetFiles()
{
	string content="";
	string line;
	ifstream myfile ("c:/data/programs/{a86f4c8f-b539-40e8-8718-62c034c997d8}/install/appManifest.xaml");
	if (myfile.is_open())
	{
		while ( myfile.good() )
		{
			getline (myfile,line);
			content+=line;
		}
		myfile.close();
	}
	else {
		content += "error";
	}
	std::wstring  oo= wstring(content.begin(), content.end());
	String^ beta = ref new String(oo.c_str());
	return beta;
}