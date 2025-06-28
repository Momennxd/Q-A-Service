
# Explanations Controller
<a id="explanations-controller"></a>

Manages explanations for question answers.

---
### Create an Explanation
<a id="create-an-explanation"></a>

> `POST /api/v1/explanations`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/explanations` | **Required** |

**Description**
Creates an explanation for a question's answer. This text is shown to users to help them understand why an answer is correct.

#### Request Structure
**Request Body** (`AnswerExplanationMainDTO`)
```json
{
  "explanationText": "string",
  "questionID": 0
}
```

#### Example Request
**Request Body**
```json
{
  "explanationText": "Mars is known as the Red Planet due to the iron oxide prevalent on its surface, which gives it a reddish appearance.",
  "questionID": 201
}
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `boolean` (`true`) indicating success. |
| `403 Forbidden` | Not Owner | You can only add explanations to your own questions. |

#### Response Structure
**`200 OK` Response Body**
```
boolean
```

#### Example Response
**`200 OK` Response Body**
```json
true
```
---
### Get Explanations for a Question
<a id="get-explanations-for-a-question"></a>

> `GET /api/v1/explanations/questions/{QuestionID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/explanations/questions/{QuestionID}` | Optional |

**Description**
Retrieves all explanations associated with a given question.

#### Request Structure
**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `QuestionID` | integer | Yes | The ID of the question. |

#### Example Request
**Request URL**
`/api/v1/explanations/questions/201`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `GetAnswerExplanationDTO` objects. |
| `404 Not Found` | Not Found | The specified question does not exist. |

#### Response Structure
**`200 OK` Response Body** (Array of `GetAnswerExplanationDTO`)
```json
[
  {
    "explanationText": "string",
    "questionID": 0,
    "addedDate": "2024-05-21T12:00:00Z"
  }
]
```

#### Example Response
**`200 OK` Response Body**
```json
[
  {
    "explanationText": "Mars is known as the Red Planet due to the iron oxide prevalent on its surface, which gives it a reddish appearance.",
    "questionID": 201,
    "addedDate": "2024-05-21T13:15:00Z"
  }
]
```
---
### Get a Single Explanation
<a id="get-a-single-explanation"></a>

> `GET /api/v1/explanations/{ExplainID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/explanations/{ExplainID}` | Optional |

**Description**
Retrieves a single explanation by its unique ID.

#### Request Structure
**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `ExplainID` | integer | Yes | The unique ID of the explanation. |

#### Example Request
**Request URL**
`/api/v1/explanations/42`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `GetAnswerExplanationDTO` object. |
| `404 Not Found` | Not Found | The explanation was not found. |

#### Response Structure
**`200 OK` Response Body** (`GetAnswerExplanationDTO`)
```json
{
  "explanationText": "string",
  "questionID": 0,
  "addedDate": "2024-05-21T12:00:00Z"
}
```

#### Example Response
**`200 OK` Response Body**
```json
{
  "explanationText": "Mars is known as the Red Planet due to the iron oxide prevalent on its surface, which gives it a reddish appearance.",
  "questionID": 201,
  "addedDate": "2024-05-21T13:15:00Z"
}
```
---
---
