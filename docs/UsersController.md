
# Users Controller
<a id="users-controller"></a>

Manages all aspects of user accounts, including registration, login, logout, and profile management.

---
### User Signup
<a id="user-signup"></a>

> `POST /api/v1/users/signup`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/users/signup` | Not Required |

**Description**
Creates a new user account with a username, password, and associated personal details. A successful registration returns the newly created user's profile information, confirming the account has been created.

#### Request Structure
**Request Body** (`AddUserDTO`)
```json
{
  "username": "string",
  "password": "string",
  "person": {
    "firstName": "string",
    "secondName": "string",
    "lastName": "string",
    "address": "string",
    "gender": true,
    "countryID": 0,
    "dateOfBirth": "2024-05-21T12:00:00Z",
    "email": "string",
    "notes": "string",
    "preferredLanguageID": 0
  }
}
```

#### Example Request
**Request Body**
```json
{
  "username": "newuser",
  "password": "aVeryStrongP@ssword123",
  "person": {
    "firstName": "Jane",
    "secondName": "M.",
    "lastName": "Doe",
    "address": "456 Oak Avenue, Anytown",
    "gender": false,
    "countryID": 2,
    "dateOfBirth": "1995-08-22T00:00:00Z",
    "email": "jane.doe@example.com",
    "notes": "New user account created via API.",
    "preferredLanguageID": 1
  }
}
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `SendUserDTO` object. |
| `400 Bad Request` | Validation Error | The request is invalid. This could be due to a username that already exists, a weak password, or other failed validation rules. |

#### Response Structure
**`200 OK` Response Body** (`SendUserDTO`)
```json
{
  "userId": 0,
  "username": "string",
  "person": {
    "firstName": "string",
    "secondName": "string",
    "lastName": "string",
    "address": "string",
    "gender": true,
    "countryID": 0,
    "dateOfBirth": "2024-05-21T12:00:00Z",
    "email": "string",
    "notes": "string",
    "preferredLanguageID": 0,
    "personID": 0,
    "joinedDate": "2024-05-21T12:00:00Z"
  }
}
```

#### Example Response
**`200 OK` Response Body**
```json
{
  "userId": 123,
  "username": "newuser",
  "person": {
    "firstName": "Jane",
    "secondName": "M.",
    "lastName": "Doe",
    "address": "456 Oak Avenue, Anytown",
    "gender": false,
    "countryID": 2,
    "dateOfBirth": "1995-08-22T00:00:00Z",
    "email": "jane.doe@example.com",
    "notes": "New user account created via API.",
    "preferredLanguageID": 1,
    "personID": 45,
    "joinedDate": "2024-05-21T12:30:00Z"
  }
}
```
---
### User Login
<a id="user-login"></a>

> `POST /api/v1/users/login`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/users/login` | Not Required |

**Description**
This is the primary endpoint for authenticating a user with their username and password. A successful login provides an `accessToken` (for API access) and a `refreshToken` (for session renewal).

#### Request Structure
**Request Body** (`LoginDTO`)
```json
{
  "username": "string",
  "password": "string"
}
```

#### Example Request
**Request Body**
```json
{
  "username": "newuser",
  "password": "aVeryStrongP@ssword123"
}
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `LoginResponseDto` object. |
| `401 Unauthorized` | Invalid Credentials | The provided username or password was incorrect. |

#### Response Structure
**`200 OK` Response Body** (`LoginResponseDto`)
```json
{
  "accessToken": "string",
  "refreshToken": "string"
}
```

#### Example Response
**`200 OK` Response Body**
```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjMiLCJuYW1lIjoibmV3dXNlciIsImlhdCI6MTUxNjIzOTAyMn0.signature",
  "refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjMiLCJ0eXBlIjoicmVmcmVzaCJ9.long_signature"
}
```
---
### External Provider Login
<a id="external-provider-login"></a>

> `POST /api/v1/users/external-login`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/users/external-login` | Not Required |

**Description**
Handles both login and registration for users authenticating with a third-party OAuth provider (e.g., Google, Facebook). Send the token provided by the external service. If a user with the associated external ID exists, they are logged in. If not, a new user account is created and then logged in.

#### Request Structure
**Request Body** (`ExternalLoginRequestDTO`)
```json
{
  "provider": "string",
  "idToken": "string"
}
```

#### Example Request
**Request Body**
```json
{
  "provider": "Google",
  "idToken": "eyJhbGciOiJSUzI1NiIsImtpZCI6IjEyMzRhYmM1Njc4OWRlZiIsImFsZyI6IlJTMjU2In0.eyJpc3MiOiJodHRwczovL2FjY291bnRzLmdvb2dsZS5jb20iLCJhenAiOiIxMjM0NTY3ODkwLWFzZGYuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJhdWQiOiIxMjM0NTY3ODkwLWFzZGYuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJzdWIiOiIxMDk4NzY1NDMyMTA5ODc2NTQzMjEiLCJlbWFpbCI6ImphbmUuZG9lQGdtYWlsLmNvbSIsImVtYWlsX3ZlcmlmaWVkIjp0cnVlLCJuYW1lIjoiSmFuZSBEb2UiLCJwaWN0dXJlIjoiaHR0cHM6Ly9saDMuZ29vZ2xldXNlcmNvbnRlbnQuY29tL2EtL0FBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUEiLCJnaXZlbl9uYW1lIjoiSmFuZSIsImZhbWlseV9uYW1lIjoiRG9lIiwibG9jYWxlIjoiZW4ifQ.signature"
}
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An `ExternalAuthResponseDTO` object. |
| `400 Bad Request` | Invalid Token | The provider token is invalid, expired, or could not be verified by the server. |

#### Response Structure
**`200 OK` Response Body** (`ExternalAuthResponseDTO`)
```json
{
  "user": {
    "userId": 0,
    "username": "string",
    "firstName": "string",
    "secondName": "string",
    "lastName": "string",
    "gender": true,
    "email": "string",
    "notes": "string",
    "userPoints": 0
  },
  "tokens": {
    "accessToken": "string",
    "refreshToken": "string"
  }
}
```

#### Example Response
**`200 OK` Response Body**
```json
{
  "user": {
    "userId": 124,
    "username": "jane.doe@gmail.com",
    "firstName": "Jane",
    "secondName": null,
    "lastName": "Doe",
    "gender": null,
    "email": "jane.doe@gmail.com",
    "notes": "Account created from Google login.",
    "userPoints": 0
  },
  "tokens": {
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjQiLCJuYW1lIjoiamFuZS5kb2VAZ21haWwuY29tIiwiaWF0IjoxNTE2MjM5MDIyfQ.signature",
    "refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjQiLCJ0eXBlIjoicmVmcmVzaCJ9.long_signature"
  }
}
```
---
### Get Current User
<a id="get-current-user"></a>

> `GET /api/v1/users`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/users` | **Required** |

**Description**
Fetches the detailed profile of the user associated with the `accessToken` sent in the `Authorization` header. This is useful for populating a user profile page or checking current user details.

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `GetUserDTO` object. |
| `401 Unauthorized` | No Token | The request lacks a valid `Authorization` header or the token is expired. |

#### Response Structure
**`200 OK` Response Body** (`GetUserDTO`)
```json
{
  "userId": 0,
  "username": "string",
  "firstName": "string",
  "secondName": "string",
  "lastName": "string",
  "gender": true,
  "email": "string",
  "notes": "string",
  "userPoints": 0
}
```

#### Example Response
**`200 OK` Response Body**
```json
{
  "userId": 123,
  "username": "newuser",
  "firstName": "Jane",
  "secondName": "M.",
  "lastName": "Doe",
  "gender": false,
  "email": "jane.doe@example.com",
  "notes": "New user account created via API.",
  "userPoints": 250
}
```
---
### Update User
<a id="update-user"></a>

> `PATCH /api/v1/users`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `PATCH` | `/api/v1/users` | **Required** |

**Description**
Allows the authenticated user to update their own profile details using the JSON Patch standard (RFC 6902). This method is efficient for making partial updates, as you only need to send the fields that are changing.

#### Request Structure
**Request Body** (Array of `Operation` objects)
```json
[
  {
    "operationType": 0,
    "path": "string",
    "op": "string",
    "from": "string",
    "value": {}
  }
]
```

#### Example Request
**Request Body**
```json
[
  {
    "op": "replace",
    "path": "/notes",
    "value": "Updated my notes."
  },
  {
    "op": "replace",
    "path": "/address",
    "value": "789 Pine Street, New City"
  }
]
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | The updated `SendUserDTO` object. |
| `400 Bad Request` | Invalid Patch | The JSON Patch document was malformed or tried to update a non-existent/protected field. |
| `401 Unauthorized` | No Token | The request lacks a valid `Authorization` header. |

#### Response Structure
**`200 OK` Response Body** (`SendUserDTO`)
```json
{
  "userId": 0,
  "username": "string",
  "person": {
    "firstName": "string",
    "secondName": "string",
    "lastName": "string",
    "address": "string",
    "gender": true,
    "countryID": 0,
    "dateOfBirth": "2024-05-21T12:00:00Z",
    "email": "string",
    "notes": "string",
    "preferredLanguageID": 0,
    "personID": 0,
    "joinedDate": "2024-05-21T12:00:00Z"
  }
}
```

#### Example Response
**`200 OK` Response Body**
```json
{
  "userId": 123,
  "username": "newuser",
  "person": {
    "firstName": "Jane",
    "secondName": "M.",
    "lastName": "Doe",
    "address": "789 Pine Street, New City",
    "gender": false,
    "countryID": 2,
    "dateOfBirth": "1995-08-22T00:00:00Z",
    "email": "jane.doe@example.com",
    "notes": "Updated my notes.",
    "preferredLanguageID": 1,
    "personID": 45,
    "joinedDate": "2024-05-21T12:30:00Z"
  }
}
```
---
### User Logout
<a id="user-logout"></a>

> `POST /api/v1/users/logout`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/users/logout` | **Required** |

**Description**
To securely log a user out, the client application must call this endpoint to revoke the current `refreshToken`. This prevents it from being used to generate new access tokens in the future. After calling this, the client should also discard the `accessToken` and `refreshToken` locally.

#### Request Structure
**Request Body** (`RefreshTokenDTO`)
```json
{
  "token": "string"
}
```

#### Example Request
**Request Body**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjMiLCJ0eXBlIjoicmVmcmVzaCJ9.long_signature"
}
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | Empty response body, confirming the token has been invalidated. |
| `400 Bad Request` | Invalid Token | The token was malformed or already invalid. |
| `401 Unauthorized` | No Token | The request lacks a valid `Authorization` header. |

---
---
