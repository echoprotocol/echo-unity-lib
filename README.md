# Echo-Unity-lib (echo-unity-lib)

*  using [websocket-sharp](https://github.com/sta/websocket-sharp)
*  using [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) (for iOS with support AOT [here](https://www.parentelement.com/assets/json_net_unity))
*  using [Base58Check](https://github.com/adamcaudill/Base58Check)
*  using [RSG.Promise](https://github.com/Real-Serious-Games/C-Sharp-Promise)

Pure C# ECHO library for Unity. Can be used to construct, sign and broadcast transactions in Unity, and to easily obtain data from the blockchain via public apis.


## Setup

Add lib to Assets folder your project.

## Preparation

Launched echo node (https://github.com/echoprotocol/echo-core) with open port

## Usage

Before run, add to your scene ConnectionManager.prefab and EchoManager.prefab from Example/Prefabs folder.
