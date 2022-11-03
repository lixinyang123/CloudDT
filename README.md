# CloudDT

Easy to use code editor with cloud development environment.

### Install pre-requisites

- .NET 6
- Node.js >= 12
- npm

##### On Windows

- Visit [.NET official website](https://dotnet.microsoft.com/) to download .NET 6.0 SDK

- Visit [Node.js official website](https://nodejs.org/) to download Node.js ( Included npm )

##### On Linux

- Install .NET SDK
    - Ubuntu
    ```bash
    apt install dotnet-sdk-6.0 -y
    ```

    - CentOS
    ```bash
    dnf install dotnet-sdk-6.0 -y
    ```


- Install Node.js & npm
    - Ubuntu
        ```bash
        apt install nodejs -y
        ```

    - CentOS
        ```bash
        dnf install nodejs -y
        ```

### Restore dependencies

```bash
cd CloudDT.Shared
dotnet restore

cd CloudDT.ContainerAPI
dotnet restore

cd CloudDT.Client
npm install
```

### Run

```bash
cd CloudDT.ContainerAPI
dotnet run

cd CloudDT.Shared
dotnet run

cd CloudDT.Client
npm start
```
