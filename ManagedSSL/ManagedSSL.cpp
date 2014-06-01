// This is the main DLL file.

#include "stdafx.h"

#include "ManagedSSL.h"
using namespace ManagedSSL;

CryptoException::CryptoException(String^ Message)
	: Exception(Message)
{
}

CryptoSession::CryptoSession(String^ EncryptKey)
{
	Int32 i, topN, topE;
	Boolean foundN = false;
	unsigned char bufferN[1024];
	unsigned char bufferE[1024];
	Int32 length = EncryptKey->Length;
	// convert the text hexadecimal stream to binary
	i = 0;
	topN = 0;
	topE = 0;
	while (i+1 < length) {
		unsigned char digits[2], value;
		if (EncryptKey[i] == L' ') {
			++i;
			continue;
		}
		else if (EncryptKey[i] == L'|') {
			++i;
			foundN = true;
			continue;
		}
		// get individual digits of byte
		for (Int32 j = 0;j < 2;++j, ++i) {
			if (EncryptKey[i]>='0' && EncryptKey[i]<='9')
				value = (unsigned char)(EncryptKey[i] - L'0');
			else if (EncryptKey[i]>='A' && EncryptKey[i]<='F')
				digits[j] = (unsigned char)(EncryptKey[i] - 'A' + 10);
			else if (EncryptKey[i]>='a' && EncryptKey[i]<='f')
				digits[j] = (unsigned char)(EncryptKey[i] - 'a' + 10);
			else
				throw gcnew CryptoException("Bad encrypt key format");
		}
		// compute value of byte
		value = (digits[0]<<8) & digits[1];
		if (!foundN) {
			if (topN >= 1024)
				throw gcnew CryptoException("Encrypt key is too large");
			bufferN[topN++] = value;
		}
		else {
			if (topE >= 1024)
				throw gcnew CryptoException("Encrypt key is too large");
			bufferE[topE++] = value;
		}
	}
	rsa = RSA_new();
	if (rsa == NULL)
		throw gcnew CryptoException("Internal SSL error");
	rsa->n = BN_bin2bn(bufferN,topN,NULL);
	rsa->e = BN_bin2bn(bufferE,topE,NULL);
	if (rsa->n == NULL)
		throw gcnew CryptoException("Public modulus was not created");
	if (rsa->e == NULL)
		throw gcnew CryptoException("Public exponent was not created");
}
CryptoSession::~CryptoSession()
{
	this->dispose(true);
}
CryptoSession::!CryptoSession()
{	
	this->dispose(false);
}
bool CryptoSession::EncryptBuffer(String^ Source,String^% Destination)
{
	if (rsa!=NULL && rsa->n!=NULL && rsa->e!=NULL) {
		Int32 length, encryptlen;
		unsigned char buffer[128], encrypted[128]; // number of bytes per encrypted "packet"
		// payload must be less than buffer
		length = Source->Length;
		if (length > 128)
			throw gcnew CryptoException("Message to encrypt is too long (break it up)");
		// convert .NET Char structures into byte-sized data
		for (Int32 i = 0;i < length;++i)
			buffer[i] = (unsigned char) Source[i];
		// encrypt buffer
		Console::WriteLine("here");
		if ((encryptlen = RSA_public_encrypt(length,buffer,encrypted,rsa,RSA_PKCS1_PADDING)) == -1)
			return false;
		// encode data as hexadecimal string
		Destination = L"";
		for (Int32 i = 0;i < encryptlen;++i) {
			unsigned char digits[2];
			digits[0] = encrypted[i] / 16;
			digits[1] = encrypted[i] % 16;
			for (Int32 j = 0;j < 2;++j) {
				if (digits[j]>=0 && digits[j]<=9)
					Destination += '0'+digits[j];
				else if (digits[j]>=10 && digits[j]<=15)
					Destination += 'A'+digits[j];
			}
			Destination += ' ';
		}
		return true;
	}
	return false;
}
void CryptoSession::dispose(bool disposing)
{
	if (rsa != NULL) { // i.e. if not disposed
		// no managed data to clean up here, just unmanaged RSA handle
		RSA_free(rsa);
		rsa = NULL;
	}
}