
# Choices Controller
<a id="choices-controller"></a>

Manages the answer choices for questions.

---
### Add Choices to Question
<a id="add-choices-to-question"></a>

> `POST /api/v1/choices`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/choices` | **Required** |

**Description**
Creates one or more new choices and associates them with an existing question.

#### Request Structure
**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `QuestionID` | integer | Yes | The ID of the question to add these choices to. |

**Request Body** (Array of `CreateChoiceDTO` objects)
```json
[
  {
    "choiceText": "string",
    "isRightAnswer": true,
    "rank": 0
  }
]
```

#### Example Request
**Request URL**
`/api/v1/choices?QuestionID=201`

**Request Body**
```json
[
  {
    "choiceText": "Earth",
    "isRightAnswer": false,
    "rank": 4
  }
]
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `SendChoiceDTO` for the newly created choices. |
| `400 Bad Request` | Invalid Input | The question ID may be missing or invalid. |
| `401 Unauthorized`| No Token | The request lacks a valid `Authorization` header. |

#### Response Structure
**`200 OK` Response Body** (Array of `SendChoiceDTO`)
```json
[
  {
    "choiceID": 0,
    "questionID": 0,
    "choiceText": "string",
    "rank": 0
  }
]
```

#### Example Response
**`200 OK` Response Body**
```json
[
  {
    "choiceID": 307,
    "questionID": 201,
    "choiceText": "Earth",
    "rank": 4
  }
]
```
---
### Get Choices by IDs
<a id="get-choices-by-ids"></a>

> `GET /api/v1/choices`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/choices` | Optional |

**Description**
This endpoint allows you to fetch details for a specific list of choices in a single call by providing their IDs in the request body.

#### Request Structure
**Request Body** (Array of integers)
```json
[
  0
]
```

#### Example Request
**Request Body**
```json
[
  301,
  302,
  305
]
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A map where keys are question IDs and values are arrays of `SendChoiceDTO` objects, grouping the choices by their parent question. |

#### Response Structure
**`200 OK` Response Body**
```json
{
  "additionalProp1": [
    {
      "choiceID": 0,
      "questionID": 0,
      "choiceText": "string",
      "rank": 0
    }
  ]
}
```

#### Example Response
**`200 OK` Response Body**
```json
{
  "201": [
    { "choiceID": 301, "questionID": 201, "choiceText": "Venus", "rank": 1 },
    { "choiceID": 302, "questionID": 201, "choiceText": "Mars", "rank": 2 }
  ],
  "202": [
    { "choiceID": 305, "questionID": 202, "choiceText": "Titan", "rank": 2 }
  ]
}
```
---
### Get Choices for a Question
<a id="get-choices-for-a-question"></a>

> `GET /api/v1/choices/questions/{questionID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/choices/questions/{questionID}` | Optional |

**Description**
Retrieves all available choices for a specific question. This is the standard way to get the options to display to a user taking a quiz. The response does not indicate which answer is correct.

#### Request Structure
**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `questionID` | integer | Yes | The ID of the question. |

#### Example Request
**Request URL**
`/api/v1/choices/questions/201`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `SendChoiceDTO` objects. |
| `404 Not Found` | Not Found | The question was not found. |

#### Response Structure
**`200 OK` Response Body** (Array of `SendChoiceDTO`)
```json
[
  {
    "choiceID": 0,
    "questionID": 0,
    "choiceText": "string",
    "rank": 0
  }
]
```

#### Example Response
**`200 OK` Response Body**
```json
[
  { "choiceID": 301, "questionID": 201, "choiceText": "Venus", "rank": 1 },
  { "choiceID": 302, "questionID": 201, "choiceText": "Mars", "rank": 2 },
  { "choiceID": 303, "questionID": 201, "choiceText": "Jupiter", "rank": 3 },
  { "choiceID": 307, "questionID": 201, "choiceText": "Earth", "rank": 4 }
]
```
---
### Get Right Answers for a Question
<a id="get-right-answers-for-a-question"></a>

> `GET /api/v1/choices/answers/{questionID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/choices/answers/{questionID}` | **Required** |

**Description**
This is a protected endpoint used for grading or showing the correct answer(s) after a user has submitted their choice. It retrieves only the choice(s) marked as correct for a given question.

#### Request Structure
**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `questionID` | integer | Yes | The ID of the question. |

#### Example Request
**Request URL**
`/api/v1/choices/answers/201`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `SendChoiceDTO` objects that are the correct answers. |
| `401 Unauthorized`| No Token | The request lacks a valid `Authorization` header. |

#### Response Structure
**`200 OK` Response Body** (Array of `SendChoiceDTO`)
```json
[
  {
    "choiceID": 0,
    "questionID": 0,
    "choiceText": "string",
    "rank": 0
  }
]
```

#### Example Response
**`200 OK` Response Body**
```json
[
  {
    "choiceID": 302,
    "questionID": 201,
    "choiceText": "Mars",
    "rank": 2
  }
]
```
---
### Get Choice with Explanation
<a id="get-choice-with-explanation"></a>

> `GET /api/v1/choices/explanation/{choiceId}/{questionId}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/choices/explanation/{choiceId}/{questionId}` | **Required** |

**Description**
After a user answers a question, use this endpoint to show them whether their choice was right or wrong, and provide the official explanation for the answer.

#### Request Structure
**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `choiceId` | integer | Yes | The ID of the choice the user selected. |
| `questionId` | integer | Yes | The ID of the question. |

#### Example Request
**Request URL**
`/api/v1/choices/explanation/302/201`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `SendChoiceWithExplanationDTO` object. |
| `401 Unauthorized`| No Token | The request lacks a valid `Authorization` header. |

#### Response Structure
**`200 OK` Response Body** (`SendChoiceWithExplanationDTO`)
```json
{
  "choiceID": 0,
  "choiceText": "string",
  "isRightAnswer": true,
  "explanationText": "string",
  "explanationID": 0
}
```

#### Example Response
**`200 OK` Response Body**
```json
{
  "choiceID": 302,
  "choiceText": "Mars",
  "isRightAnswer": true,
  "explanationText": "Mars is known as the Red Planet due to the iron oxide prevalent on its surface, which gives it a reddish appearance.",
  "explanationID": 42
}
```
---
### Update a Choice
<a id="update-a-choice"></a>

> `PATCH /api/v1/choices/{ChoiceID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `PATCH` | `/api/v1/choices/{ChoiceID}` | **Required** |

**Description**
Updates specific fields of an existing choice (e.g., its text or rank) using the JSON Patch standard.

#### Request Structure
**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `ChoiceID` | integer | Yes | The ID of the choice to update. |

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
**Request URL**
`/api/v1/choices/302`

**Request Body**
```json
[
  {
    "op": "replace",
    "path": "/choiceText",
    "value": "The Red Planet (Mars)"
  }
]
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | The updated `SendChoiceDTO` object. |
| `403 Forbidden` | Not Owner | You do not own the parent question. |

#### Response Structure
**`200 OK` Response Body** (`SendChoiceDTO`)
```json
{
  "choiceID": 0,
  "questionID": 0,
  "choiceText": "string",
  "rank": 0
}
```

#### Example Response
**`200 OK` Response Body**
```json
{
  "choiceID": 302,
  "questionID": 201,
  "choiceText": "The Red Planet (Mars)",
  "rank": 2
}
```
---
### Delete a Choice
<a id="delete-a-choice"></a>

> `DELETE /api/v1/choices/{ChoiceID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `DELETE` | `/api/v1/choices/{ChoiceID}` | **Required** |

**Description**
Permanently deletes a choice from a question.

#### Request Structure
**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `ChoiceID` | integer | Yes | The ID of the choice to delete. |

#### Example Request
**Request URL**
`/api/v1/choices/307`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An `integer` representing the ID of the deleted choice. |
| `403 Forbidden` | Not Owner | You do not own the parent question. |

#### Response Structure
**`200 OK` Response Body**
```
integer
```

#### Example Response
**`200 OK` Response Body**
```json
307
```
---
---
