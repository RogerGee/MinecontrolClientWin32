// ManagedSSL.h

#pragma once

using namespace System;

namespace ManagedSSL {

    public ref class CryptoException : Exception
    {
    public:
        CryptoException(String^ Message);
    };

    public ref class CryptoSession : IDisposable
    {
    public:
        CryptoSession(String^ EncryptKey);
        ~CryptoSession();
        !CryptoSession();   

        Boolean EncryptBuffer(String^ Source,String^% Destination);
    private:
        RSA* rsa;

        void dispose(bool disposing);
    };
}
