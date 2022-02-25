# Spike [21748](https://dev.azure.com/CRISPpointsolutions/PointSolutions/_sprints/taskboard/AyyTeam/PointSolutions/A79%20-%202.21.2022?workitem=21748)

## Background 
The health records proxy application is a demonstration web application that is capable of retrieving `CRISP Specific FHIR Resources` by id using existing API's and underlying datastores.

The goal is to provide an efficient mechanism that can retrieve `Observations`, `Specimens`, and `ImagingStudy` CRISP FHIR Resources and normalize the response to match the view required by in-context.

The web application has 2 controllers with that illustrate _HOW_ to perform a batch search from the microservices.
The application has static get methods for demonstrating data retrieval and normalization to assess differences in the output and response deltas.

The for each Controller there is a `GetAllExample`  action method that will transform the input in request and process it.
Below is an example of the a list of [HealthRecordRequest](src/CRISP.HealthRecordsProxy.Common/APIModels/ServiceResponse.cs) that specify the 
resource name and logical id's to search for. 

The precise way that InContext can change 

```json
[
    {
        "resourceType": "Observation",
        "logicalIdentifier": [
            "21748c07-aa58-a460-8518-1a93f48f424f",
            "c1258861-497d-d89b-c833-17e5cbb246f5",
            "73b70122-b0f9-5ef9-c55f-9855d0f0ab99",
            "7f509afb-6e6a-e11f-6250-4aaeab51a1bf",
            "5c0d29a3-7d67-7441-8901-cec65afad2ae",
            "d2112447-4d52-ad41-7d6a-a9ab63ce5ea9",
            "54f4b253-c2e2-ef81-afda-2a9919411e59",
            "429ffbd4-0059-3dad-468a-abb56c82ff85",
            "692537fb-c2e8-1fe1-bcb5-bc81a80e18a6",
            "f20005db-3c5d-817a-6afe-3de73488ee9e"
        ]
    },
    {
        "resourceType": "Specimen",
        "logicalIdentifier": [
            "ff50d2a0-e3ee-5cbe-916e-e0f5a535cdc1",
            "b3d86f8b-8fdd-c1f6-f7c4-3ac21cd66418",
            "44d79f55-f0df-5059-d6ba-f677092c1ea0",
            "0e08feea-2db7-8f02-1471-351962f3974d",
            "9b4841de-1ef5-8b5c-9d23-e12048572186",
            "aeaeae5b-4db2-3bf6-a7cf-a84810b85612",
            "603d3038-4aa8-d5fd-93f3-a3a020862202",
            "5439307f-9f67-a701-651f-639fe3cf4b57",
            "cb7125ab-c516-bdf1-22b1-80141d1815a7",
            "e4a6a8c4-e7c1-d5fc-b231-4507ba3a193f"
        ]
    },
    {
        "resourceType": "ImagingStudy",
        "logicalIdentifier": [
            "5a1cac61-b61c-b1c3-01d2-5857a83438eb",
            "f1e7cdb4-b48c-a858-c4cd-0906a6dd7929",
            "466fad63-faf0-e42f-acfa-2b1c5d9c272d",
            "d45174e0-0e58-ee54-be84-33c855425358",
            "418c3fa6-427c-b2c5-b1a1-59f38f482c67",
            "5f495b98-c896-a848-9c57-6b862c89ef7e",
            "bae3627b-1e5f-23f3-5e92-6dae624dbcc0",
            "b5ac5b2e-7b40-21b8-a8f8-7a310df04a2d",
            "cf841eba-38c9-0e85-eeba-889935675298",
            "6da1a00d-6d7f-55f2-a0a2-ce70392ef068"
        ]
    }
]
```


### Resource Controller - API Workflow

The `Resource` Controller Represents an example mechanism to call for  `Observations`, `Specimens`, and `ImagingStudy` resources directly 
from the microservice. This pathway is the _ALMOST IDENTICAL_ to the way diagnostic report pulls the contained resources with the query parameter

`_include=DiagnosticReport:result`
- Performs the get request to the observation microservice

`_include=DiagnosticReport:specimen`
- Performs the get request to the specimen microservice

`_include=DiagnosticReport:imagingStudy`
- Performs the get request to the imaging study microservice

The following endpoint are exposed to fetch individual resources:
```http request
###
GET https://localhost:5001/Resource/GetImagingStudyExample

###
GET https://localhost:5001/Resource/GetObservationExample

###
GET https://localhost:5001/Resource/GetSpecimenExample
```