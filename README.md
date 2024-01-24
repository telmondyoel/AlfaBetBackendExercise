# AlfaBet Backend Exercise

### Prerequisites before running: <br>

- An [ASP.NET Core 8 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-8.0.1-windows-x64-installer)
- A running PostgreSQL environment

### Running the project

> **Note before running:** The database connection can be set with the `ConnectionStrings__AlfaBetDb` environment variable <br>
> ```bash
> # Example 
> set ConnectionStrings__AlfaBetDb="Host=localhost;Username=postgres;Password=postgres;Database=alfabet"
> ```

<br>

Run the project in `release` mode with the `dotnet` cli from the root of the project like so:

```bash
dotnet run --project .\AlfaBetBackendExercise\AlfaBetBackendExercise.csproj -c Release --launch-profile "https-release"
```

<br>

The project can also be run in `development` mode:

```bash
dotnet run --project .\AlfaBetBackendExercise\AlfaBetBackendExercise.csproj -c Release --launch-profile "https-dev"
```

Running in development mode will enable Swagger UI, which can be found at `https://localhost:8081/swagger/` after starting the project in this mode

You can validate the application is up and running with the `alive` api: `https://localhost:8081/alive/`

---

### Authentication

After starting the project for the first time, registering a user is required to be able to use the events apis. <br>
Do this by registering through the `/register` api.

> Example
> ```bash
> curl -i -k -H "Content-Type: application/json" -d "{\"email\":\"test@user.com\",\"password\":\"Password123!\"}" -X POST https://localhost:8081/register
> ```

Once registered, you will need to login with the `/login` api.

> Example
> ```bash
> curl -i -k -H "Content-Type: application/json" -d "{\"email\":\"test@user.com\",\"password\":\"Password123!\"}" -X POST https://localhost:8081/login
> ```

A response from the `/login` api should look something like this:

```json
{
  "tokenType": "Bearer",
  "accessToken": "CfDJ8LO-UpYPlxdCs7xR9LhCFSReK4aAzNhd6ugCUKIZqZMD1AGRu_p3NYq-w2S5BSchb-naNe3oax4MJTt6W6fY56CAzgCH0QnWZGwg7HgBz_A_WiisnX1ZPPdOPIx7sJYgnfVkyeTgX2ovNivdwQk05fLew0l6GoXMe_1oe9dUVP0LGtQi9sWSc9hu3ss73nn4Bp6AjDD4egpVAKukItgbI_PYcZUu-qNZx0O088GiPr2ML2Dg5dxokLjxz1YF78Gz__p4PEhaKXq4CsXKfv3-W_Kdb6w9gLXtKbijw5kY0TLf2CxGoDKQXYYtIhqtxxRd3esmy4Gy3o-zE_IfTDD8KNT17OZYD1XmP6-hcyklt0mKnTPyHaVDyMhi3O_ejq7cbSU7gzLXUsxc0mpFNmZPUQQmN5gpyq2DEJvEql1ar3rWWULCgrJaQlsUR5UaDXWL3AYCU4HohsVynDOO4pQSn27KQwhQWIZXQ5h7L9o6PboAzd-5jDqcZntd7zeZUtY5RQqVMtBSt3D-EhUHsRrHj3BeqGFuJjRgfoxiJnlrmzxOpbxXTnxZWvYeloS7xwlWlImV5KhtUZw01aSZmjH0m35ouSe4d0DyzNklaKjS5COYW6yn2_TCmGjg2WWjYLejwvF1k07coZQGjS7CYSjdnAgfCxRjzBwvN_tw9yDRcUjZju86r70bb2Y6608_-87o1w",
  "expiresIn": 3600,
  "refreshToken": "CfDJ8LO-UpYPlxdCs7xR9LhCFSRrXSmOEB-_X2dZ2bXZeaPjmqXbk9oPPnFRY_Q2HVIVT9Hh0muOCTqBXQe21VBQPCT90qOkXuomlokraUIR8MLH3Z7VGVSrCUtWROvJfOM66xNeyht-oSqtA-_F6Y2wBScuTBqZ_jmAt-NKaopE5re6u79Dz4xkXzf4z3vRb-cmMe8ut34LYq4j1BkTQ4owi6Cy3wAA-bbzdnFs0oxKEj6iul0J5DdRHpmZT3-lzM3V-goARJCtrMhSe3IALwHZi4q3VSPOktbQ01YbRqt1Tx8FUXmYhP1Hjc4X8TJihy1hrDx4t3HVi32jCkGOXX2gCpdrOpnDFiDjo9PZXA8janmdorO4EXXaf73UCLje8Y1RzaYQubc3k893PYYR3g1OckSrxcZn75f9dhPTgfJNspLDr-I9Q0lc-BMIQEbtHy-zuAcVdZvA7qoFWzJFRmEyLjrJ8VNr0bACX2NpnmACVulp26pz00_R3Nyi258t-HtJ1QNFAXGmLhED0mQ-nmWYhGOenkjdPwYtyE2yblqbOJOTlI_ezQ4iUL1cRAcAH_R5k03s_SzMwx_7D9KIZ-xnIDhUz0MXa-9EIhQvwD_Gby6ciAGlFJZ3n1Gb8aFSyrWYH-n3HzCHmJwtoN4dJeMQT3j5SzmDJ3RE5Qi8BVMjgoCxTDw0Tva1MwJiLdiSBrwcww"
}
```

The `accessToken` value will be needed to authenticate as a bearer token when accessing `events` apis. <br>
If the login session expires, you can simply login again to receive a new `accessToken`.

---

### Reminders

Reminders are simulated, and if a reminder needs to be "sent", it will simply be printed to the console. <br>
However, regardless of the simulated-ness, the architecture for implementing additional "reminder sinks" was kept in mind with the Open/Closed SOLID
principle. <br>
For instance, an `EmailEventReminderSink` class could be easily written to send out emails to people subscribed to events.

---

### Api documentation

[See here](AlfaBetBackendExercise/Documentation/apis.md) for the full api documentation