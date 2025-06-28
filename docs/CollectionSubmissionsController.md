
# Collection Submissions Controller
<a id="collection-submissions-controller"></a>

Manages the lifecycle of a user attempting a collection (e.g., taking a quiz).

---
### Create a Submission
<a id="create-a-submission"></a>

> `POST /api/v1/submitions`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/submitions` | **Required** |

**Description**
Before a user starts answering questions in a collection, call this endpoint to create a "submission" container. The returned `submitionID` is crucial and must be used to associate all subsequent answers with this specific attempt.

#### Request Structure
**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `CollectionID` | integer | Yes | The ID of the collection being attempted. |

#### Example Request
**Request URL**
`/api/v1/submitions?CollectionID=101`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An `integer` representing the new `submitionID`. |
| `401 Unauthorized`| No Token | The request lacks a valid `Authorization` header. |
| `404 Not Found` | Not Found | The specified collection does not exist. |

#### Response Structure
**`200 OK` Response Body**
```
integer
```

#### Example Response
**`200 OK` Response Body**
```json
55
```
---
### Get Submission Details
<a id="get-submission-details"></a>

> `GET /api/v1/submitions`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/submitions` | **Required** |

**Description**
Retrieves the details and final results of a specific submission attempt after it has been completed.

#### Request Structure
**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `SubmissionID` | integer | Yes | The ID of the submission to retrieve, obtained from the [Create a Submission](#create-a-submission) endpoint. |

#### Example Request
**Request URL**
`/api/v1/submitions?SubmissionID=55`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `CollectionSubmissionMainDTO` object. |
| `403 Forbidden` | Not Owner | You can only view your own submissions. |
| `404 Not Found` | Not Found | The specified submission does not exist. |

#### Response Structure
**`200 OK` Response Body** (`CollectionSubmissionMainDTO`)
```json
{
  "submitDate": "2024-05-21T12:00:00Z",
  "username": "string",
  "totalChosenChoices": 0,
  "totalRightAnswers": 0
}
```

#### Example Response
**`200 OK` Response Body**
```json
{
  "submitDate": "2024-05-21T16:00:00Z",
  "username": "newuser",
  "totalChosenChoices": 10,
  "totalRightAnswers": 8
}
```
---
### Delete a Submission
<a id="delete-a-submission"></a>

> `DELETE /api/v1/submitions`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `DELETE` | `/api/v1/submitions` | **Required** |

**Description**
Deletes a user's submission record. This might be used to allow a user to clear their attempt history.

#### Request Structure
**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `SubmitionID` | integer | Yes | The ID of the submission to delete. |

#### Example Request
**Request URL**
`/api/v1/submitions?SubmitionID=55`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | Empty response body. |
| `403 Forbidden` | Not Owner | You can only delete your own submissions. |
| `404 Not Found` | Not Found | The specified submission does not exist. |

---
---
