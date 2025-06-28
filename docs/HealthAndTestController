

# Health & Test Controller
<a id="health--test-controller"></a>

Endpoints for system health checks and internal/developer testing. These endpoints are generally not for public client consumption.

---
### Health Check
<a id="health-check"></a>

> `GET /api/health`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/health` | Not Required |

**Description**
A simple endpoint to verify that the API service is running and reachable. It can be used by monitoring services to check application health.

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | Empty response body, indicating the service is healthy. |

---
### Get Secure Metrics
<a id="get-secure-metrics"></a>

> `GET /api/secure-metrics`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/secure-metrics` | **Required** (Likely Admin-only) |

**Description**
An authenticated endpoint for retrieving system metrics. This is likely restricted to administrative users.

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | Varies. Likely a JSON or text-based metrics report. |
| `401 Unauthorized`| No Token | The request lacks a valid `Authorization` header. |
| `403 Forbidden` | Insufficient Permissions | The user does not have the required permissions to view metrics. |

---
### Get Claims (Test)
<a id="get-claims-test"></a>

> `GET /API/Test/GetClaims`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/API/Test/GetClaims` | **Required** |

**Description**
A test endpoint for developers to inspect the claims contained within their current `accessToken`.

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A representation of the JWT claims. |

---
### Create New Token (Test)
<a id="create-new-token-test"></a>

> `POST /API/Test/CreateNewToken`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/API/Test/CreateNewToken` | **Required** (Likely Admin-only) |

**Description**
A test endpoint for developers to generate a new token for a specific user ID, bypassing the normal login flow.

#### Request Structure
**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `userID` | integer | Yes | The ID of the user to generate a token for. |

#### Example Request
**Request URL**
`/API/Test/CreateNewToken?userID=123`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A new token. |

---
### Upload Image (Test)
<a id="upload-image-test"></a>

> `POST /API/Test/UploadImage`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/API/Test/UploadImage` | **Required** |

**Description**
A test endpoint for developers to test the file upload functionality.

#### Request Structure
**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `folderPath` | string | No | The destination folder path on the server. |
| `fileName` | string | No | The desired file name on the server. |

**Request Body** (`multipart/form-data`)
The request must be a multipart form data request containing the file to be uploaded.

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | Empty response body. |

---
### Test Critical Log (Test)
<a id="test-critical-log-test"></a>

> `GET /API/Test/TestCriticalLog`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/API/Test/TestCriticalLog` | **Required** (Likely Admin-only) |

**Description**
A test endpoint for developers to trigger a critical-level log event to ensure the logging and monitoring systems are working correctly.

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | Empty response body. |

---
### External Login (Test)
<a id="external-login-test"></a>

> `POST /API/Test/external-login`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/API/Test/external-login` | Not Required |

**Description**
A test endpoint for developers to simulate an external login flow.

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
  "provider": "TestProvider",
  "idToken": "test-token-12345"
}
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | Varies based on test implementation. |
