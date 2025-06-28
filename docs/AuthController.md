
# Auth Controller
<a id="auth-controller"></a>

Endpoints for handling authentication-related tasks, specifically refreshing access tokens.

---
### Refresh Token
<a id="refresh-token"></a>

> `POST /api/v1/auth/refresh-token`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/auth/refresh-token` | **Not Required** (Token is in body) |

**Description**
When a user's `accessToken` expires, the client application should use this endpoint to get a new one without forcing the user to log in again. Provide the `refreshToken` obtained during the initial login. A successful call returns a new pair of access and refresh tokens.

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
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyLCJ0eXBlIjoicmVmcmVzaCJ9.some_long_signature"
}
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `TokenResponseDto` object. |
| `404 Not Found` | Invalid Token | The provided refresh token is invalid, expired, or has been revoked. The user must log in again. |

#### Response Structure
**`200 OK` Response Body** (`TokenResponseDto`)
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
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuZXdfYWNjZXNzX3Rva2VuIjoiLi4uIn0.signature",
  "refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuZXdfcmVmcmVzaF90b2tlbiI6Ii4uLiJ9.signature"
}
```
---
---
