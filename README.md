
# Healthcare Provider Appointment Scheduling System (HPASS)




## Project Overview
![HPASS project overview](https://github.com/ureturkmert/casestudy/assets/139451860/3338bea8-f9a4-42a6-8faa-d6d0b47d89a9)

Ideally, in real life scenario, different solutions should be formed and library access should be made throught CI / CD pipeline (Teamcity etc...) and nuget packages. For the sake of simplicity and proof of concept, all task made in a single solution with various projects to simulate layers in the architecture.

**HPASS.Service.Main** project created with .NET6 WEB API template. Modified to show some samples about how I design a back end service for current requirements and future development.

As stated in manual, Ef Core used as orm with code first application. Since no database design or model requirements stated in detail, I design models with theoretical knowhow on healthcare sector.

![appsettings](https://github.com/ureturkmert/casestudy/assets/139451860/03e32408-0d90-49f3-b580-1b708c51db44)

Project developed to work with PostgreSql V15.3 and target database can be set throught **appsettings.json** file as shown

Exported **Postman Test Collection** can be reach by [link](https://drive.google.com/file/d/1tFqR_1m2bzEZlVJYBNY-0EuIS_v7nC8Z/view?usp=sharing) or exported json file can be coppied from down there then can be imported to test service.

For further information about how to import data into Postman [link](https://learning.postman.com/docs/getting-started/importing-and-exporting-data/#importing-data-into-postman) would be helpful.



```json

{
	"info": {
		"_postman_id": "67e7bf91-0d1c-4393-a74d-780597521409",
		"name": "Case Study Postman Collection",
		"schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json"
	},
	"item": [
		{
			"name": "System Related Requests",
			"item": [
				{
					"name": "Heart Beat",
					"request": {
						"method": "GET",
						"header": [],
						"url": {
							"raw": "http://localhost:5015/system/heartbeat",
							"protocol": "http",
							"host": [
								"localhost"
							],
							"port": "5015",
							"path": [
								"system",
								"heartbeat"
							]
						}
					},
					"response": []
				},
				{
					"name": "Generate Initial Databse",
					"request": {
						"method": "GET",
						"header": [],
						"url": {
							"raw": "http://localhost:5015/System/GenerateInitialDatabase",
							"protocol": "http",
							"host": [
								"localhost"
							],
							"port": "5015",
							"path": [
								"System",
								"GenerateInitialDatabase"
							]
						}
					},
					"response": []
				}
			]
		},
		{
			"name": "Authentication Related Requests",
			"item": [
				{
					"name": "Login",
					"event": [
						{
							"listen": "test",
							"script": {
								"exec": [
									"const jsonResponse = pm.response.json();\r",
									"pm.globals.set(\"oauth_token\", jsonResponse.result.token);\r",
									""
								],
								"type": "text/javascript"
							}
						}
					],
					"request": {
						"method": "POST",
						"header": [],
						"body": {
							"mode": "raw",
							"raw": "{\r\n    \"username\":\"guventest\",\r\n    \"password\":\"guventest\"\r\n\r\n    //\"username\":\"acibademtest\",\r\n    //\"password\":\"acibademtest\"\r\n}",
							"options": {
								"raw": {
									"language": "json"
								}
							}
						},
						"url": {
							"raw": "http://localhost:5015/Authentication/Login",
							"protocol": "http",
							"host": [
								"localhost"
							],
							"port": "5015",
							"path": [
								"Authentication",
								"Login"
							]
						}
					},
					"response": []
				}
			]
		},
		{
			"name": "Operation Zone Related Requests",
			"item": [
				{
					"name": "Paging Operation Zone",
					"request": {
						"method": "POST",
						"header": [
							{
								"key": "Authorization",
								"value": "Bearer {{oauth_token}}",
								"type": "default"
							}
						],
						"body": {
							"mode": "raw",
							"raw": "{\r\n    \"pageSize\": 20,\r\n    \"pageNumber\": 0\r\n}",
							"options": {
								"raw": {
									"language": "json"
								}
							}
						},
						"url": {
							"raw": "http://localhost:5015/Main/PagingOperationZones",
							"protocol": "http",
							"host": [
								"localhost"
							],
							"port": "5015",
							"path": [
								"Main",
								"PagingOperationZones"
							]
						}
					},
					"response": []
				},
				{
					"name": "Create Operation Zone",
					"request": {
						"method": "POST",
						"header": [
							{
								"key": "Authorization",
								"value": "Bearer {{oauth_token}}",
								"type": "default"
							}
						],
						"body": {
							"mode": "raw",
							"raw": "{\r\n    \"name\": \"New Operation Zone\"\r\n}",
							"options": {
								"raw": {
									"language": "json"
								}
							}
						},
						"url": {
							"raw": "http://localhost:5015/Main/CreateOperationZone",
							"protocol": "http",
							"host": [
								"localhost"
							],
							"port": "5015",
							"path": [
								"Main",
								"CreateOperationZone"
							],
							"query": [
								{
									"key": "",
									"value": null,
									"disabled": true
								}
							]
						}
					},
					"response": []
				},
				{
					"name": "Update Operation Zone",
					"request": {
						"method": "POST",
						"header": [
							{
								"key": "Authorization",
								"value": "Bearer {{oauth_token}}",
								"type": "default"
							}
						],
						"body": {
							"mode": "raw",
							"raw": "{\r\n    \"updatingOperationZoneId\": \"6dabc89d-b9ba-466b-a2a2-c8cc18b026c6\",\r\n    \"name\": \"updated zone name\"\r\n}",
							"options": {
								"raw": {
									"language": "json"
								}
							}
						},
						"url": {
							"raw": "http://localhost:5015/Main/UpdateOperationZone",
							"protocol": "http",
							"host": [
								"localhost"
							],
							"port": "5015",
							"path": [
								"Main",
								"UpdateOperationZone"
							]
						}
					},
					"response": []
				}
			]
		},
		{
			"name": "Patient Related Requests",
			"item": [
				{
					"name": "Create Patient",
					"request": {
						"method": "POST",
						"header": [
							{
								"key": "Authorization",
								"value": "Bearer {{oauth_token}}",
								"type": "default"
							}
						],
						"body": {
							"mode": "raw",
							"raw": "{\r\n\r\n    \"name\":\"Patient\",\r\n    \"surname\":\"Test\",\r\n    \"age\":20,\r\n    \"heigth\":170,\r\n    \"weight\":80,\r\n    \"nationalIdentifier\":\"3334444221\",\r\n    \"genderType\":2,// Female = 0, Unkown = 1, Male = 2\r\n    \"phone\":\"053212312312\",\r\n    \"email\":\"test@gmail.com\"\r\n}",
							"options": {
								"raw": {
									"language": "json"
								}
							}
						},
						"url": {
							"raw": "http://localhost:5015/Main/CreatePatient",
							"protocol": "http",
							"host": [
								"localhost"
							],
							"port": "5015",
							"path": [
								"Main",
								"CreatePatient"
							]
						}
					},
					"response": []
				},
				{
					"name": "Paging Patients",
					"request": {
						"method": "POST",
						"header": [
							{
								"key": "Authorization",
								"value": "Bearer {{oauth_token}}",
								"type": "default"
							}
						],
						"body": {
							"mode": "raw",
							"raw": "{\r\n    \"pageSize\": 20,\r\n    \"pageNumber\": 0\r\n}",
							"options": {
								"raw": {
									"language": "json"
								}
							}
						},
						"url": {
							"raw": "http://localhost:5015/Main/PagingPatients",
							"protocol": "http",
							"host": [
								"localhost"
							],
							"port": "5015",
							"path": [
								"Main",
								"PagingPatients"
							]
						}
					},
					"response": []
				},
				{
					"name": "Update Patient",
					"request": {
						"method": "POST",
						"header": [
							{
								"key": "Authorization",
								"value": "Bearer {{oauth_token}}",
								"type": "default"
							}
						],
						"body": {
							"mode": "raw",
							"raw": "{\r\n    \"updatingPatientId\": \"f9fdef3c-c789-4168-a118-1663fa6221bf\",\r\n    \"name\": \"Patient Update\",\r\n    \"surname\": \"Test Update\",\r\n    \"age\": 20,\r\n    \"heigth\": 170,\r\n    \"weight\": 80,\r\n    \"nationalIdentifier\": \"555555\",\r\n    \"genderType\": 1, // Female = 0, Unkown = 1, Male = 2\r\n    \"phone\": \"456456456456\",\r\n    \"email\": \"update@gmail.com\"\r\n}",
							"options": {
								"raw": {
									"language": "json"
								}
							}
						},
						"url": {
							"raw": "http://localhost:5015/Main/UpdatePatient",
							"protocol": "http",
							"host": [
								"localhost"
							],
							"port": "5015",
							"path": [
								"Main",
								"UpdatePatient"
							]
						}
					},
					"response": []
				}
			]
		},
		{
			"name": "Doctor Related Requests",
			"item": [
				{
					"name": "Get Doctors Of Health Provider",
					"request": {
						"method": "GET",
						"header": [
							{
								"key": "Authorization",
								"value": "Bearer {{oauth_token}}",
								"type": "default"
							}
						],
						"url": {
							"raw": "http://localhost:5015/Main/GetDoctorsOfHealthProvider",
							"protocol": "http",
							"host": [
								"localhost"
							],
							"port": "5015",
							"path": [
								"Main",
								"GetDoctorsOfHealthProvider"
							]
						}
					},
					"response": []
				}
			]
		},
		{
			"name": "Appointment Related Requests",
			"item": [
				{
					"name": "Assign Apppointment To Patient",
					"request": {
						"method": "POST",
						"header": [
							{
								"key": "Authorization",
								"value": "Bearer {{oauth_token}}",
								"type": "default"
							}
						],
						"body": {
							"mode": "raw",
							"raw": "{\r\n\r\n    \"patientId\":\"6215ef5e-cd61-44f4-a0e7-c4a1e07a0f93\",\r\n    \"doctorId\":\"b82ad334-98cd-4a7e-abd8-49d8f60edfd4\",\r\n    \"operationZoneId\":\"e9ca953f-1701-4025-99d9-663e243f169a\",\r\n    \"date\":\"2023-07-20T09:15:00.000000Z\"\r\n\r\n}",
							"options": {
								"raw": {
									"language": "json"
								}
							}
						},
						"url": {
							"raw": "http://localhost:5015/Main/AssignAppointmentToPatient",
							"protocol": "http",
							"host": [
								"localhost"
							],
							"port": "5015",
							"path": [
								"Main",
								"AssignAppointmentToPatient"
							]
						}
					},
					"response": []
				},
				{
					"name": "Update Appointment Status",
					"request": {
						"method": "POST",
						"header": [
							{
								"key": "Authorization",
								"value": "Bearer {{oauth_token}}",
								"type": "default"
							}
						],
						"body": {
							"mode": "raw",
							"raw": "{\r\n    \"appointmentId\": \"29a2538d-cec1-438d-b627-cf6814b1d916\",\r\n    \"newStatus\": 1 // Created = 0 , Canceled = 1 , Completed = 2\r\n}",
							"options": {
								"raw": {
									"language": "json"
								}
							}
						},
						"url": {
							"raw": "http://localhost:5015/Main/UpdateAppointmentStatus",
							"protocol": "http",
							"host": [
								"localhost"
							],
							"port": "5015",
							"path": [
								"Main",
								"UpdateAppointmentStatus"
							]
						}
					},
					"response": []
				},
				{
					"name": "Get Appointments Of Patient",
					"request": {
						"method": "POST",
						"header": [
							{
								"key": "Authorization",
								"value": "Bearer {{oauth_token}}",
								"type": "default"
							}
						],
						"body": {
							"mode": "raw",
							"raw": "{\r\n    \"id\":\"6215ef5e-cd61-44f4-a0e7-c4a1e07a0f93\"\r\n}",
							"options": {
								"raw": {
									"language": "json"
								}
							}
						},
						"url": {
							"raw": "http://localhost:5015/Main/GetAppointmentsOfPatient",
							"protocol": "http",
							"host": [
								"localhost"
							],
							"port": "5015",
							"path": [
								"Main",
								"GetAppointmentsOfPatient"
							]
						}
					},
					"response": []
				}
			]
		},
		{
			"name": "Medical History Related Requests",
			"item": [
				{
					"name": "Provide Medical History For Patient",
					"request": {
						"method": "POST",
						"header": [
							{
								"key": "Authorization",
								"value": "Bearer {{oauth_token}}",
								"type": "default"
							}
						],
						"body": {
							"mode": "raw",
							"raw": "{\r\n    \"patientId\": \"6215ef5e-cd61-44f4-a0e7-c4a1e07a0f93\",\r\n    \"header\": \"Ateşli Hastalık Geçmişi\",\r\n    \"description\": \"Yok\"\r\n}",
							"options": {
								"raw": {
									"language": "json"
								}
							}
						},
						"url": {
							"raw": "http://localhost:5015/Main/ProvideMedicalHistoryForPatient",
							"protocol": "http",
							"host": [
								"localhost"
							],
							"port": "5015",
							"path": [
								"Main",
								"ProvideMedicalHistoryForPatient"
							]
						}
					},
					"response": []
				},
				{
					"name": "Get Medical History Of Patient",
					"request": {
						"method": "POST",
						"header": [
							{
								"key": "Authorization",
								"value": "Bearer {{oauth_token}}",
								"type": "default"
							}
						],
						"body": {
							"mode": "raw",
							"raw": "{\r\n    \"id\": \"6215ef5e-cd61-44f4-a0e7-c4a1e07a0f93\"\r\n}",
							"options": {
								"raw": {
									"language": "json"
								}
							}
						},
						"url": {
							"raw": "http://localhost:5015/Main/GetMedicalHistoryOfPatient",
							"protocol": "http",
							"host": [
								"localhost"
							],
							"port": "5015",
							"path": [
								"Main",
								"GetMedicalHistoryOfPatient"
							]
						}
					},
					"response": []
				}
			]
		}
	]
}

```





## Postman Collection

Once you import provided Postman Collection you will reach test methods.

![image](https://github.com/ureturkmert/casestudy/assets/139451860/0a05d832-922f-4934-9458-6875c9170b3e)

Once target database access information set service can be run.
For initial run **Generate Initial Databse** test method can be used to create database and mock data. 

Each time method is called database will be reset to default state and records. Since entity ID's designed as GUID each time you generate initial data id of records will be change! 

Keep that in mind while exploring rest of the collection methods. Requests will require valid ID values for future manipulation

![image](https://github.com/ureturkmert/casestudy/assets/139451860/48ff5557-7edc-4e6b-897d-477aea7c44b0)


All requests other than **System Related Requests** and **Authentication Related Requests** are guarded with jwt authentication by service.Login method will authenticate simulating client and store obtanied token in global variables of Postman

![image](https://github.com/ureturkmert/casestudy/assets/139451860/9a264412-dbcc-428c-ae59-45ee9c7bf2fa)

![image](https://github.com/ureturkmert/casestudy/assets/139451860/fa4d76df-c01d-475a-b57c-6027babf8d8b)


After login is made rest of the methods can be called without thinking authentication. Tokens will be valid for 1 Hour after they are obtained!

For further code review and detailed explanation I am looking forward for your call.

With Kind Regards,
Mert ÜRETÜRK

