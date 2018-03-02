# Mobile-BankId-.NET-Example
# - version 4 (WSDL) and version 5 (REST)
Working Swedish Mobile BankID implementation written in C# .NET

Documentation on version 4 (WSDL) is available at http://www.herlitz.nu/2017/09/13/integrating-with-swedish-bankid-and-.net/
and https://www.bankid.com/assets/bankid/rp/bankid-relying-party-guidelines-v2.16.pdf .
Documentation on version 5 (REST) is available at https://www.bankid.com/bankid-i-dina-tjanster/rp-info ,
namely https://www.bankid.com/assets/bankid/rp/bankid-relying-party-guidelines-v3.0.pdf .

BankID APIs require client and server certificates installed.
You can find public server certificate in https://www.bankid.com/assets/bankid/rp/bankid-relying-party-guidelines-v3.0.pdf ,
while test private client certificate can be downloaded from https://www.bankid.com/assets/bankid/rp/FPTestcert2_20150818_102329.pfx .
You also need to install the test mobile application https://www.bankid.com/assets/bankid/rp/BankID_7.8.61_BGC_CUSTOMERTEST.apk .
