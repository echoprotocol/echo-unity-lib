### ECC
The ECC library contains all the crypto functions for private and public keys as well as transaction creation/signing.

#### Private keys
As a quick example, here's how to generate a new private key from a seed:

```c#
private class PassContainer : IPass
{
    private readonly string password;


    public PassContainer(string password)
    {
        this.password = password;
    }

    public void Dispose() => password = null;

    public byte[] Get() => Encoding.UTF8.GetBytes(password.Trim());
}

...

var userName = "testusername42";
var role = AuthorityClassification.Active;
var password = new PassContainer("P5KDbEubFQS4cNtimMMnTL6tkM4nqWDXjEjhmQDrxGvoY");
var keys = new KeyPair(role, userName, password, ECDSA.KeyFactory.Create());
Debug.Log("Private key: " + keys.Private.ToWif());
Debug.Log("Public key: " + keys.Public);
```

#### Echorand keys (ED25519)
As a quick example, here's how to generate a rand key from a seed:

```c#
var factory = EDDSA.KeyFactory.Create();
var privateKey = new byte[0];
var publicKey = new byte[0];
ED25519REF10.ED25519.CreateKeyPair(out publicKey, out privateKey);
var keys = new KeyPair(factory.FromSeed(privateKey));
Debug.Log("Private key: " + keys.Private.ToWif());
Debug.Log("Public key: " + keys.Public);
```

#### Random keys
As a quick example, here's how to generate a random key:

```c#
var privateKey = new byte[0];
var publicKey = new byte[0];
ED25519REF10.ED25519.CreateKeyPair(out publicKey, out privateKey);
var key = EDDSA.PrivateKey.FromBuffer(privateKey);
Debug.Log("Random private key: " + key.ToWif());
Debug.Log("Random public key: " + key.PublicKey);
```
