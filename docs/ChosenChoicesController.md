
# Chosen Choices Controller
<a id="chosen-choices-controller"></a>

Manages the recording and retrieval of user answers during a submission attempt.

---
### Submit a Chosen Answer
<a id="submit-a-chosen-answer"></a>

> `POST /api/v1/choices/chosen`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/choices/chosen` | **Required** |

**Description**
As a user answers each question during a quiz, call this endpoint to log their selected choice. This links the user's answer to their specific submission attempt.

#### Request Structure
**Request Body** (`Add_chosen_choicesDTO`)
```json
{
  "choiceID": 0,
  "submitionID": 0
}
```

#### Example Request
**Request Body**
```json
{
  "choiceID": 302,
  "submitionID": 55
}
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `send_chosen_choicesDTO` object confirming the record was created. |
| `403 Forbidden` | Not Owner | You cannot add choices to someone else's submission. |

#### Response Structure
**`200 OK` Response Body** (`send_chosen_choicesDTO`)
```json
{
  "chosen_ChoiceID": 0,
  "choiceID": 0,
  "userID": 0,
  "chosenDate": "2024-05-21T12:00:00Z",
  "submitionID": 0
}
```

#### Example Response
**`200 OK` Response Body**
```json
{
  "chosen_ChoiceID": 901,
  "choiceID": 302,
  "userID": 123,
  "chosenDate": "2024-05-21T16:05:10Z",
  "submitionID": 55
}
```
---
### Get Submitted Answers
<a id="get-submitted-answers"></a>

> `GET /api/v1/choices/chosen/submition/{submitionID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/choices/chosen/submition/{submitionID}` | **Required** |

**Description**
Retrieves all the choices a user made for a given submission. This is useful for displaying a summary of a user's answers after a quiz is complete.

#### Request Structure
**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `submitionID` | integer | Yes | The ID of the submission. |

**Header Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `QuestionIDs` | Array of integers | No | An optional comma-separated list of question IDs to filter the results for. |

#### Example Request
**Request URL**
`/api/v1/choices/chosen/submition/55`

**Headers**
`QuestionIDs: 201,202`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A map where keys are question IDs and values are `send_chosen_choicesDTO` objects. |
| `403 Forbidden` | Not Owner | You can only view your own chosen choices. |

#### Response Structure
**`200 OK` Response Body**
```json
{
  "additionalProp1": {
    "chosen_ChoiceID": 0,
    "choiceID": 0,
    "userID": 0,
    "chosenDate": "2024-05-21T12:00:00Z",
    "submitionID": 0
  }
}
```

#### Example Response
**`200 OK` Response Body**
```json
{
  "201": {
    "chosen_ChoiceID": 901,
    "choiceID": 302,
    "userID": 123,
    "chosenDate": "2024-05-21T16:05:10Z",
    "submitionID": 55
  },
  "202": {
    "chosen_ChoiceID": 902,
    "choiceID": 306,
    "userID": 123,
    "chosenDate": "2024-05-21T16:05:25Z",
    "submitionID": 55
  }
}
```
---
---
