
# Question Categories Controller
<a id="question-categories-controller"></a>

Manages the relationship between questions and categories.

---
### Get Categories for a Question
<a id="get-categories-for-a-question"></a>

> `GET /api/v1/categories/questions/{QuestionID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/categories/questions/{QuestionID}` | Optional |

**Description**
Retrieves all categories that have been associated with a specific question.

#### Request Structure
**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `QuestionID` | integer | Yes | The ID of the question. |

#### Example Request
**Request URL**
`/api/v1/categories/questions/201`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `SendQuestionsCategoryDTO` objects. |
| `404 Not Found` | Not Found | The specified question does not exist. |

#### Response Structure
**`200 OK` Response Body** (Array of `SendQuestionsCategoryDTO`)
```json
[
  {
    "question_CategoryID": 0,
    "questionID": 0,
    "categoryID": 0,
    "categoryName": "string"
  }
]
```

#### Example Response
**`200 OK` Response Body**
```json
[
  {
    "question_CategoryID": 501,
    "questionID": 201,
    "categoryID": 10,
    "categoryName": "Science"
  }
]
```
---
### Add Categories to a Question
<a id="add-categories-to-a-question"></a>

> `POST /api/v1/categories/questions/{QuestionID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/categories/questions/{QuestionID}` | **Required** |

**Description**
Associates one or more existing categories with a question. This "tags" the question, making it discoverable.

#### Request Structure
**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `QuestionID` | integer | Yes | The ID of the question to add categories to. |

**Request Body** (Array of `CreateQuestionsCategoryDTO` objects)
```json
[
  {
    "categoryID": 0
  }
]
```

#### Example Request
**Request URL**
`/api/v1/categories/questions/201`

**Request Body**
```json
[
  { "categoryID": 10 },
  { "categoryID": 11 }
]
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An `integer` representing the number of new associations created. |
| `403 Forbidden` | Not Owner | You do not own the question. |

#### Response Structure
**`200 OK` Response Body**
```
integer
```

#### Example Response
**`200 OK` Response Body**
```json
2
```
---
### Delete All Categories from a Question
<a id="delete-all-categories-from-a-question"></a>

> `DELETE /api/v1/categories/questions/{QuestionID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `DELETE` | `/api/v1/categories/questions/{QuestionID}` | **Required** |

**Description**
Removes all category associations from a single question. This is a bulk "untag" operation.

#### Request Structure
**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `QuestionID` | integer | Yes | The ID of the question to remove all category links from. |

#### Example Request
**Request URL**
`/api/v1/categories/questions/201`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An `integer` representing the number of associations removed. |
| `403 Forbidden` | Not Owner | You do not own the question. |

#### Response Structure
**`200 OK` Response Body**
```
integer
```

#### Example Response
**`200 OK` Response Body**
```json
2
```
---
### Delete a Question-Category Link
<a id="delete-a-question-category-link"></a>

> `DELETE /api/v1/categories/{QuestionCategoryID}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `DELETE` | `/api/v1/categories/{QuestionCategoryID}` | **Required** |

**Description**
Removes a single, specific association between a question and a category. This does not delete the question or the category itself, only the link between them.

#### Request Structure
**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `QuestionCategoryID` | integer | Yes | The unique ID of the question-category link (the `question_CategoryID` from the `SendQuestionsCategoryDTO` object). |

#### Example Request
**Request URL**
`/api/v1/categories/501`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `boolean` (`true`) indicating success. |
| `403 Forbidden` | Not Owner | You do not own the question. |
| `404 Not Found` | Not Found | The specified question-category link does not exist. |

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
---
