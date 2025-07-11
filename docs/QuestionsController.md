
# Questions Controller
<a id="questions-controller"></a>

Manages the questions that belong to collections.

---
### Add Questions to Collection
<a id="add-questions-to-collection"></a>

> `POST /api/v1/questions`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/questions` | **Required** |

**Description**
Use this endpoint to add one or more new questions to a collection that has already been created. This is useful for building a collection incrementally.

#### Request Structure
**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `CollectionID` | integer | Yes | The ID of the collection to add questions to. |

**Request Body** (Array of `CreateQuestionDTO` objects)
```json
[
  {
    "questionText": "string",
    "isMCQ": true,
    "questionPoints": 0,
    "rank": 0,
    "choices": [
      {
        "choiceText": "string",
        "isRightAnswer": true,
        "rank": 0
      }
    ]
  }
]
```

#### Example Request
**Request URL**
`/api/v1/questions?CollectionID=101`

**Request Body**
```json
[
  {
    "questionText": "What is the largest moon of Saturn?",
    "isMCQ": true,
    "questionPoints": 15,
    "rank": 2,
    "choices": [
      { "choiceText": "Europa", "isRightAnswer": false, "rank": 1 },
      { "choiceText": "Titan", "isRightAnswer": true, "rank": 2 },
      { "choiceText": "Ganymede", "isRightAnswer": false, "rank": 3 }
    ]
  }
]
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `SendQuestionDTO` for the newly created questions. |
| `403 Forbidden` | Not Owner | You do not own the collection you are trying to modify. |

#### Response Structure
**`200 OK` Response Body** (Array of `SendQuestionDTO`)
```json
[
  {
    "questionID": 0,
    "questionText": "string",
    "userID": 0,
    "isMCQ": true,
    "addedDate": "2024-05-21T12:00:00Z",
    "questionPoints": 0,
    "rank": 0,
    "choices": [
      {
        "choiceID": 0,
        "questionID": 0,
        "choiceText": "string",
        "rank": 0
      }
    ]
  }
]
```

#### Example Response
**`200 OK` Response Body**
```json
[
  {
    "questionID": 202,
    "questionText": "What is the largest moon of Saturn?",
    "userID": 123,
    "isMCQ": true,
    "addedDate": "2024-05-21T13:10:00Z",
    "questionPoints": 15,
    "rank": 2,
    "choices": [
      { "choiceID": 304, "questionID": 202, "choiceText": "Europa", "rank": 1 },
      { "choiceID": 305, "questionID": 202, "choiceText": "Titan", "rank": 2 },
      { "choiceID": 306, "questionID": 202, "choiceText": "Ganymede", "rank": 3 }
    ]
  }
]
```
---
### Get Questions from Collection
<a id="get-questions-from-collection"></a>

> `GET /api/v1/questions`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/questions` | Optional |

**Description**
Retrieves all questions for a given collection. This is a simplified endpoint and may not be the primary way to fetch questions for a quiz (see `GET /api/v1/collections/{CollecID}`).

#### Request Structure
**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `CollectionID` | integer | Yes | The ID of the collection whose questions to fetch. |

#### Example Request
**Request URL**
`/api/v1/questions?CollectionID=101`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `SendQuestionDTO` object (Note: The schema suggests a single object, which might be an error. It typically should be an array). |
| `404 Not Found` | Not Found | The specified collection does not exist. |

#### Response Structure
**`200 OK` Response Body** (`SendQuestionDTO`)
```json
{
  "questionID": 0,
  "questionText": "string",
  "userID": 0,
  "isMCQ": true,
  "addedDate": "2024-05-21T12:00:00Z",
  "questionPoints": 0,
  "rank": 0,
  "choices": [
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
  "questionID": 201,
  "questionText": "Which planet is known as the Red Planet?",
  "userID": 123,
  "isMCQ": true,
  "addedDate": "2024-05-21T13:00:00Z",
  "questionPoints": 10,
  "rank": 1,
  "choices": [
    { "choiceID": 301, "questionID": 201, "choiceText": "Venus", "rank": 1 },
    { "choiceID": 302, "questionID": 201, "choiceText": "Mars", "rank": 2 },
    { "choiceID": 303, "questionID": 201, "choiceText": "Jupiter", "rank": 3 }
  ]
}
```
---
### Delete a Question
<a id="delete-a-question"></a>

> `DELETE /api/v1/questions`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `DELETE` | `/api/v1/questions` | **Required** |

**Description**
Permanently deletes a question and its associated choices.

#### Request Structure
**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `QuestionID` | integer | Yes | The ID of the question to delete. |

#### Example Request
**Request URL**
`/api/v1/questions?QuestionID=202`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An `integer` representing the ID of the deleted question. |
| `403 Forbidden` | Not Owner | You do not own the question. |
| `404 Not Found` | Not Found | The specified question does not exist. |

#### Response Structure
**`200 OK` Response Body**
```
integer
```

#### Example Response
**`200 OK` Response Body**
```json
202
```
---
### Get a Single Question
<a id="get-a-single-question"></a>

> `GET /api/v1/questions/{QuestionID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/questions/{QuestionID}` | Optional |

**Description**
Retrieves a single question by its unique ID, including its choices.

#### Request Structure
**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `QuestionID` | integer | Yes | The unique ID of the question. |

#### Example Request
**Request URL**
`/api/v1/questions/201`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `SendQuestionDTO` object. |
| `404 Not Found` | Not Found | A question with the specified ID was not found. |

#### Response Structure
**`200 OK` Response Body** (`SendQuestionDTO`)
```json
{
  "questionID": 0,
  "questionText": "string",
  "userID": 0,
  "isMCQ": true,
  "addedDate": "2024-05-21T12:00:00Z",
  "questionPoints": 0,
  "rank": 0,
  "choices": [
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
  "questionID": 201,
  "questionText": "Which planet is known as the Red Planet?",
  "userID": 123,
  "isMCQ": true,
  "addedDate": "2024-05-21T13:00:00Z",
  "questionPoints": 10,
  "rank": 1,
  "choices": [
    { "choiceID": 301, "questionID": 201, "choiceText": "Venus", "rank": 1 },
    { "choiceID": 302, "questionID": 201, "choiceText": "Mars", "rank": 2 },
    { "choiceID": 303, "questionID": 201, "choiceText": "Jupiter", "rank": 3 }
  ]
}
```
---
### Update a Question
<a id="update-a-question"></a>

> `PATCH /api/v1/questions/{QuestionID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `PATCH` | `/api/v1/questions/{QuestionID}` | **Required** |

**Description**
Updates specific fields of an existing question (e.g., its text or rank) using the JSON Patch standard.

#### Request Structure
**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `QuestionID` | integer | Yes | The unique ID of the question to update. |

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
`/api/v1/questions/201`

**Request Body**
```json
[
  {
    "op": "replace",
    "path": "/questionText",
    "value": "Which planet in our solar system is famously known as the Red Planet?"
  }
]
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | The updated `SendQuestionDTO` object. |
| `403 Forbidden` | Not Owner | You do not own the question you are trying to modify. |

#### Response Structure
**`200 OK` Response Body** (`SendQuestionDTO`)
```json
{
  "questionID": 0,
  "questionText": "string",
  "userID": 0,
  "isMCQ": true,
  "addedDate": "2024-05-21T12:00:00Z",
  "questionPoints": 0,
  "rank": 0,
  "choices": [
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
  "questionID": 201,
  "questionText": "Which planet in our solar system is famously known as the Red Planet?",
  "userID": 123,
  "isMCQ": true,
  "addedDate": "2024-05-21T13:00:00Z",
  "questionPoints": 10,
  "rank": 1,
  "choices": [
    { "choiceID": 301, "questionID": 201, "choiceText": "Venus", "rank": 1 },
    { "choiceID": 302, "questionID": 201, "choiceText": "Mars", "rank": 2 },
    { "choiceID": 303, "questionID": 201, "choiceText": "Jupiter", "rank": 3 }
  ]
}
```
---
### Update Question Points
<a id="update-question-points"></a>

> `PATCH /api/v1/questions/points/{QuestionID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `PATCH` | `/api/v1/questions/points/{QuestionID}` | **Required** |

**Description**
A dedicated, lightweight endpoint to quickly update the point value of a question.

#### Request Structure
**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `QuestionID` | integer | Yes | The ID of the question to update. |

**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `NewPointsVal` | integer | Yes | The new point value for the question. |

#### Example Request
**Request URL**
`/api/v1/questions/points/201?NewPointsVal=15`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An `integer` representing the updated point value. |
| `403 Forbidden` | Not Owner | You do not own the question. |

#### Response Structure
**`200 OK` Response Body**
```
integer
```

#### Example Response
**`200 OK` Response Body**
```json
15
```
---
### Get Random Questions
<a id="get-random-questions"></a>

> `GET /api/v1/questions/random`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/questions/random` | Optional |

**Description**
Fetches a set of random questions from a specific collection. This is useful for creating a dynamic quiz experience where the questions are not always in the same order or for sampling a large collection.

#### Request Structure
**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `collectionId` | integer | Yes | The ID of the collection to pull random questions from. |

#### Example Request
**Request URL**
`/api/v1/questions/random?collectionId=101`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `QuestionWithChoicesDto` objects. This DTO is lightweight and does not include the `isRightAnswer` field. |
| `404 Not Found` | Not Found | The specified collection does not exist. |

#### Response Structure
**`200 OK` Response Body** (Array of `QuestionWithChoicesDto`)
```json
[
  {
    "questionID": 0,
    "questionText": "string",
    "rank": 0,
    "choices": [
      {
        "choiceID": 0,
        "choiceText": "string"
      }
    ]
  }
]
```

#### Example Response
**`200 OK` Response Body**
```json
[
  {
    "questionID": 201,
    "questionText": "Which planet is known as the Red Planet?",
    "rank": 1,
    "choices": [
      { "choiceID": 301, "choiceText": "Venus" },
      { "choiceID": 302, "choiceText": "Mars" },
      { "choiceID": 303, "choiceText": "Jupiter" }
    ]
  }
]
```

---
### Get Right Answer and Explanation for a Question
<a id="get-right-answer-and-explanation-for-a-question"></a>

> `GET /api/v1/questions/right-answer-with-explanation/{questionID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/questions/right-answer-with-explanation/{questionID}` | **Required** |

**Description**
Retrieves the correct answer choice ID and an optional explanation for a specific question. This is useful for providing feedback to users after they have answered a question. Requires authentication to access.

#### Request Structure
**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `questionID` | integer | Yes | The unique ID of the question for which to retrieve the right answer and explanation. |

#### Example Request
**Request URL**
`/api/v1/questions/right-answer-with-explanation/201`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `SendExplanationWithRightAnswerDTO` object containing the ID of the correct answer choice and an optional explanation. |
| `401 Unauthorized` | Unauthorized | Authentication credentials are required. |

#### Response Structure
**`200 OK` Response Body** (`SendExplanationWithRightAnswerDTO`)
```json
{
  "rightAnswerChoiceID": 0,
  "explanationText": "string",
  "explanationID": 0
}
```

#### Example Response
**`200 OK` Response Body**
```json
{
  "rightAnswerChoiceID": 302,
  "explanationText": "Mars is often called the Red Planet due to the presence of iron oxide on its surface, which gives it a reddish appearance.",
  "explanationID": 401
}
```
---
---

