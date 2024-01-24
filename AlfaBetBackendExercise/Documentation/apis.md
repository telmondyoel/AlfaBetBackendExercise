# Api Documentation

---

## Alive

#### Check if application is running

<details>
<summary><code>GET</code><code>/alive</code></summary>

##### Responses

> | http code | content-type               | response     |
> |-----------|----------------------------|--------------|
> | `200`     | `text/plain;charset=utf-8` | `I'm alive!` |

##### Example cURL

> ```shell
> curl -i -k -X GET https://localhost:8081/alive
> ```

</details>

---

## Authentication

#### Register a new user

<details>
<summary><code>POST</code><code>/register</code></summary>

##### Body

> ```
> {
>   "email": <valid email string>,
>   "password": <string that contains at least [1 uppercase letter], [1 lowercase letter], [1 number], & [1 special character]>
> }
> ```

##### Responses

> | http code | content-type               | response                                                   |
> |-----------|----------------------------|------------------------------------------------------------|
> | `200`     | None                       | None                                                       |
> | `400`     | `application/problem+json` | `{"status":"400","errors":<dictionary of various errors>}` |

##### Example cURL

> ```shell
> curl -i -k -H "Content-Type: application/json" -d "{\"email\":\"test@user.com\",\"password\":\"Password123!\"}" -X POST https://localhost:8081/register
> ```

</details>

<br>

#### Login

<details>
<summary><code>POST</code><code>/login</code></summary>

##### Body

> ```
> {
>   "email": <string>,
>   "password": <string>
> }
> ```

##### Responses

> | http code | content-type                     | response                                                                                                                                                                |
> |-----------|----------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
> | `200`     | `application/json;charset=utf-8` | `{"tokenType":"Bearer","accessToken":<access token string for authentication>,"expiresIn":<when accessToken expires in seconds>,"refreshToken":<refresh token string>}` |
> | `401`     | `application/problem+json`       | `{"title":"Unauthorized","status":"401","detail":"Failed"}`                                                                                                             |

##### Example cURL

> ```shell
> curl -i -k -H "Content-Type: application/json" -d "{\"email\":\"test@user.com\",\"password\":\"Password123!\"}" -X POST https://localhost:8081/login
> ```

</details>

---

## Events

#### Schedule event

<details>
<summary><code>POST</code><code>/events</code></summary>

##### Headers

> | key             | value                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        |
> |-----------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
> | `Authorization` | `<valid bearer token>`<br/><br/>Example:<br/>`Bearer CfDJ8LO-UpYPlxdCs7xR9LhCFSQ9JhXCbljg8Ol5FgbF1M_vziQZ6C4KbXziWmfgSnylxCdrUXbHxcKobyxVChQZGyuXT0T8mo3jlnMSxdv_nGloF-SCS8JNzzc5IsRnvv_WhCDlTOqrStT4GaHoExbIacl9uJ0sWFrLLyfAb07F4DioP-qhoGnRaGIOO-TbPdxgXuxHkW7Cfdtidu43d4uWI-njj6zU00DJP_cVAgE5w9btvfXjnfRCyyaRp8tuK6Z5sRQ-XWU8qirqkM-FJL_42J1aTXARZCsWTtOBGksyXinrbeV-ZOdlw4VKl5kTtmCujJ_XkKWqdnz4CXaPQkocA1W8pcPMNHwr1gzWvCLu2q2mmb0ipCs_HL3prkRbH5OFhO74CoTnsQlJLM58dEebXDcz97c8VYInghnf7_YSh_hOiEULlrbdaL0ytTGarlJxYIrVoPiQLq6TiZVUp84pak-VUlDWCgBFJ2St9clTdTmUahaJeEtbWXxrbbUcog6eviXnaFpBlXTKX4vFjbCYBVJqbSScBpn9WtpYV70ovLmI65BE8ZO4ZglZexR4nnulxXFoYpR8mvL4Il_sZIoYoqmPfGwkwXRJyOedx77a9lcYS2_7_fHIXm_Ufq7KCvrv4yPA83tjVa64VohiE9fbnZOMmDJ_nUTcnMm9MASAR4-10w3_ic2MnaItcom9QmIe1A` |

##### Body

> ```
> {
>   "summary": <string>,
>   "location": <string>,
>   "date: <UTC ISO datetime string in the following format: [yyyy-MM-ddTHH:mm:ss.fffZ] | Example: "2024-01-31T17:30:00.0Z">,
>   "participants": <integer>
> }
> ```

##### Responses

> | http code | content-type                     | response                                                                                                                                                      |
> |-----------|----------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------|
> | `201`     | `application/json;charset=utf-8` | `{"id":<event id>,"summary":<string>,"location":<string>,"date":<UTC ISO datetime string>,"participants":<integer>,"creationDate":<UTC ISO datetime string>}` |
> | `400`     | `application/problem+json`       | `{"status":"400","errors":<dictionary of various errors>}`                                                                                                    |
> | `401`     | None                             | None                                                                                                                                                          |

##### Example cURL

> ```shell
> curl -i -k -H "Content-Type: application/json" -H "Authorization: Bearer <replace with access token>" -d "{\"summary\":\"Football match with the lads\",\"location\":\"Football pitch by the loch\",\"date\":\"2024-02-13T17:30:00.0Z\",\"participants\":22}" -X POST https://localhost:8081/events
> ```

</details>

<br>

#### Retrieve events

<details>
<summary><code>GET</code><code>/events</code></summary>

##### Headers

> | key             | value                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        |
> |-----------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
> | `Authorization` | `<valid bearer token>`<br/><br/>Example:<br/>`Bearer CfDJ8LO-UpYPlxdCs7xR9LhCFSQ9JhXCbljg8Ol5FgbF1M_vziQZ6C4KbXziWmfgSnylxCdrUXbHxcKobyxVChQZGyuXT0T8mo3jlnMSxdv_nGloF-SCS8JNzzc5IsRnvv_WhCDlTOqrStT4GaHoExbIacl9uJ0sWFrLLyfAb07F4DioP-qhoGnRaGIOO-TbPdxgXuxHkW7Cfdtidu43d4uWI-njj6zU00DJP_cVAgE5w9btvfXjnfRCyyaRp8tuK6Z5sRQ-XWU8qirqkM-FJL_42J1aTXARZCsWTtOBGksyXinrbeV-ZOdlw4VKl5kTtmCujJ_XkKWqdnz4CXaPQkocA1W8pcPMNHwr1gzWvCLu2q2mmb0ipCs_HL3prkRbH5OFhO74CoTnsQlJLM58dEebXDcz97c8VYInghnf7_YSh_hOiEULlrbdaL0ytTGarlJxYIrVoPiQLq6TiZVUp84pak-VUlDWCgBFJ2St9clTdTmUahaJeEtbWXxrbbUcog6eviXnaFpBlXTKX4vFjbCYBVJqbSScBpn9WtpYV70ovLmI65BE8ZO4ZglZexR4nnulxXFoYpR8mvL4Il_sZIoYoqmPfGwkwXRJyOedx77a9lcYS2_7_fHIXm_Ufq7KCvrv4yPA83tjVa64VohiE9fbnZOMmDJ_nUTcnMm9MASAR4-10w3_ic2MnaItcom9QmIe1A` |

##### Parameters

> | name             | type     | data type | description                            | possible values                              | default value |
> |------------------|----------|-----------|----------------------------------------|----------------------------------------------|---------------|
> | `locationFilter` | optional | string    | Location substring to filter events by |                                              |               |
> | `sortBy`         | optional | string    | Event value to sort by                 | `date`, `popularity`, `creationdate`         |               |
> | `sortOrder`      | optional | string    | Sort order/direction to sort by        | `ascending` or `asc`, `descending` or `desc` | `ascending`   |

##### Responses

> | http code | content-type                     | response                                                                                                                                                                       |
> |-----------|----------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
> | `200`     | `application/json;charset=utf-8` | List of events `[{"id":<event id>,"summary":<string>,"location":<string>,"date":<UTC ISO datetime string>,"participants":<integer>,"creationDate":<UTC ISO datetime string>}]` |
> | `400`     | `application/problem+json`       | `{"status":"400","errors":<dictionary of various errors>}`                                                                                                                     |                                                                                                                                                                       
< | `401`     | None                             | None                                                                                                                                                                           |

##### Example cURL

> ```shell
> # List of all events
> curl -i -k -H "Authorization: Bearer <replace with access token>" -X GET https://localhost:8081/events
> ```
> ```shell
> # Filtered and sorted list of events
> curl -i -k -H "Authorization: Bearer <replace with access token>" -X GET "https://localhost:8081/events?locationFilter=pitch&sortBy=date2&sortOrder=DESC"
> ```

</details>

<br>

#### Retrieve specific event

<details>
<summary><code>GET</code><code>/events/{eventId}</code></summary>

##### Headers

> | key             | value                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        |
> |-----------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
> | `Authorization` | `<valid bearer token>`<br/><br/>Example:<br/>`Bearer CfDJ8LO-UpYPlxdCs7xR9LhCFSQ9JhXCbljg8Ol5FgbF1M_vziQZ6C4KbXziWmfgSnylxCdrUXbHxcKobyxVChQZGyuXT0T8mo3jlnMSxdv_nGloF-SCS8JNzzc5IsRnvv_WhCDlTOqrStT4GaHoExbIacl9uJ0sWFrLLyfAb07F4DioP-qhoGnRaGIOO-TbPdxgXuxHkW7Cfdtidu43d4uWI-njj6zU00DJP_cVAgE5w9btvfXjnfRCyyaRp8tuK6Z5sRQ-XWU8qirqkM-FJL_42J1aTXARZCsWTtOBGksyXinrbeV-ZOdlw4VKl5kTtmCujJ_XkKWqdnz4CXaPQkocA1W8pcPMNHwr1gzWvCLu2q2mmb0ipCs_HL3prkRbH5OFhO74CoTnsQlJLM58dEebXDcz97c8VYInghnf7_YSh_hOiEULlrbdaL0ytTGarlJxYIrVoPiQLq6TiZVUp84pak-VUlDWCgBFJ2St9clTdTmUahaJeEtbWXxrbbUcog6eviXnaFpBlXTKX4vFjbCYBVJqbSScBpn9WtpYV70ovLmI65BE8ZO4ZglZexR4nnulxXFoYpR8mvL4Il_sZIoYoqmPfGwkwXRJyOedx77a9lcYS2_7_fHIXm_Ufq7KCvrv4yPA83tjVa64VohiE9fbnZOMmDJ_nUTcnMm9MASAR4-10w3_ic2MnaItcom9QmIe1A` |

##### Parameters

> | name        | type     | data type | description                     | possible values                              | default value |
> |-------------|----------|-----------|---------------------------------|----------------------------------------------|---------------|
> | `eventId`   | required | int       | Event id                        |                                              |               |

##### Responses

> | http code | content-type                     | response                                                                                                                                                      |
> |-----------|----------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------|
> | `200`     | `application/json;charset=utf-8` | `{"id":<event id>,"summary":<string>,"location":<string>,"date":<UTC ISO datetime string>,"participants":<integer>,"creationDate":<UTC ISO datetime string>}` |                                                                                                                                                                       
> | `401`     | None                             | None                                                                                                                                                          |
> | `404`     | `text/plain;charset=utf-8`       | `Event {eventId} does not exist`                                                                                                                              |

##### Example cURL

> ```shell
> curl -i -k -H "Authorization: Bearer <replace with access token>" -X GET https://localhost:8081/events/1
> ```

</details>

<br>

#### Update specific event

<details>
<summary><code>PUT</code><code>/events/{eventId}</code></summary>

##### Headers

> | key             | value                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        |
> |-----------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
> | `Authorization` | `<valid bearer token>`<br/><br/>Example:<br/>`Bearer CfDJ8LO-UpYPlxdCs7xR9LhCFSQ9JhXCbljg8Ol5FgbF1M_vziQZ6C4KbXziWmfgSnylxCdrUXbHxcKobyxVChQZGyuXT0T8mo3jlnMSxdv_nGloF-SCS8JNzzc5IsRnvv_WhCDlTOqrStT4GaHoExbIacl9uJ0sWFrLLyfAb07F4DioP-qhoGnRaGIOO-TbPdxgXuxHkW7Cfdtidu43d4uWI-njj6zU00DJP_cVAgE5w9btvfXjnfRCyyaRp8tuK6Z5sRQ-XWU8qirqkM-FJL_42J1aTXARZCsWTtOBGksyXinrbeV-ZOdlw4VKl5kTtmCujJ_XkKWqdnz4CXaPQkocA1W8pcPMNHwr1gzWvCLu2q2mmb0ipCs_HL3prkRbH5OFhO74CoTnsQlJLM58dEebXDcz97c8VYInghnf7_YSh_hOiEULlrbdaL0ytTGarlJxYIrVoPiQLq6TiZVUp84pak-VUlDWCgBFJ2St9clTdTmUahaJeEtbWXxrbbUcog6eviXnaFpBlXTKX4vFjbCYBVJqbSScBpn9WtpYV70ovLmI65BE8ZO4ZglZexR4nnulxXFoYpR8mvL4Il_sZIoYoqmPfGwkwXRJyOedx77a9lcYS2_7_fHIXm_Ufq7KCvrv4yPA83tjVa64VohiE9fbnZOMmDJ_nUTcnMm9MASAR4-10w3_ic2MnaItcom9QmIe1A` |

##### Parameters

> | name        | type     | data type | description                     | possible values                              | default value |
> |-------------|----------|-----------|---------------------------------|----------------------------------------------|---------------|
> | `eventId`   | required | int       | Event id                        |                                              |               |

##### Body

> ```
> {
>   "summary": <string>,
>   "location": <string>,
>   "date: <UTC ISO datetime string in the following format: [yyyy-MM-ddTHH:mm:ss.fffZ] | Example: "2024-01-31T17:30:00.0Z">,
>   "participants": <integer>
> }
> ```

##### Responses

> | http code | content-type                     | response                                                                                                                                                      |
> |-----------|----------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------|
> | `200`     | `application/json;charset=utf-8` | `{"id":<event id>,"summary":<string>,"location":<string>,"date":<UTC ISO datetime string>,"participants":<integer>,"creationDate":<UTC ISO datetime string>}` |
> | `400`     | `application/problem+json`       | `{"status":"400","errors":<dictionary of various errors>}`                                                                                                    |
> | `401`     | None                             | None                                                                                                                                                          |
> | `404`     | `text/plain;charset=utf-8`       | `Event {eventId} does not exist`                                                                                                                              |

##### Example cURL

> ```shell
> curl -i -k -H "Content-Type: application/json" -H "Authorization: Bearer <replace with access token>" -d "{\"summary\":\"Soccer game with the Americans\",\"location\":\"Soccer field by the Dunkin' Donut\",\"date\":\"2024-02-13T17:30:00.0Z\",\"participants\":22}" -X PUT https://localhost:8081/events/1
> ```

</details>

<br>

#### Delete specific event

<details>
<summary><code>DELETE</code><code>/events/{eventId}</code></summary>

##### Headers

> | key             | value                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        |
> |-----------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
> | `Authorization` | `<valid bearer token>`<br/><br/>Example:<br/>`Bearer CfDJ8LO-UpYPlxdCs7xR9LhCFSQ9JhXCbljg8Ol5FgbF1M_vziQZ6C4KbXziWmfgSnylxCdrUXbHxcKobyxVChQZGyuXT0T8mo3jlnMSxdv_nGloF-SCS8JNzzc5IsRnvv_WhCDlTOqrStT4GaHoExbIacl9uJ0sWFrLLyfAb07F4DioP-qhoGnRaGIOO-TbPdxgXuxHkW7Cfdtidu43d4uWI-njj6zU00DJP_cVAgE5w9btvfXjnfRCyyaRp8tuK6Z5sRQ-XWU8qirqkM-FJL_42J1aTXARZCsWTtOBGksyXinrbeV-ZOdlw4VKl5kTtmCujJ_XkKWqdnz4CXaPQkocA1W8pcPMNHwr1gzWvCLu2q2mmb0ipCs_HL3prkRbH5OFhO74CoTnsQlJLM58dEebXDcz97c8VYInghnf7_YSh_hOiEULlrbdaL0ytTGarlJxYIrVoPiQLq6TiZVUp84pak-VUlDWCgBFJ2St9clTdTmUahaJeEtbWXxrbbUcog6eviXnaFpBlXTKX4vFjbCYBVJqbSScBpn9WtpYV70ovLmI65BE8ZO4ZglZexR4nnulxXFoYpR8mvL4Il_sZIoYoqmPfGwkwXRJyOedx77a9lcYS2_7_fHIXm_Ufq7KCvrv4yPA83tjVa64VohiE9fbnZOMmDJ_nUTcnMm9MASAR4-10w3_ic2MnaItcom9QmIe1A` |

##### Parameters

> | name        | type     | data type | description                     | possible values                              | default value |
> |-------------|----------|-----------|---------------------------------|----------------------------------------------|---------------|
> | `eventId`   | required | int       | Event id                        |                                              |               |

##### Responses

> | http code | content-type               | response                                   |
> |-----------|----------------------------|--------------------------------------------|
> | `200`     | `text/plain;charset=utf-8` | `Event {eventId} was deleted successfully` |
> | `401`     | None                       | None                                       |
> | `404`     | `text/plain;charset=utf-8` | `Event {eventId} does not exist`           |

##### Example cURL

> ```shell
> curl -i -k -H "Authorization: Bearer <replace with access token>" -X DELETE https://localhost:8081/events/1
> ```

</details>