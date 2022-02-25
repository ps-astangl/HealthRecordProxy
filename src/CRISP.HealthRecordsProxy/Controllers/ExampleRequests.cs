using System;
using System.Linq;
using CRISP.HealthRecordsProxy.Common.APIModels;
using Hl7.Fhir.Model;

namespace CRISP.HealthRecordProxy.Controllers
{
    public static class ExampleRequests
    {
        public static HealthRecordsRequest OneHundredObservations()
        {
           var ids = new string[]
            {
                "0098196e-894b-e035-7529-ccabe4717d1d",
                "8ee2ec1a-b471-4131-4a4c-d4f11eb48b55",
                "8aa111b5-561f-163f-cabb-4e8240df7c6b",
                "b3e8a1d2-84f6-3d48-9591-7497303f1069",
                "c42bb39d-816b-e904-e927-8cc2385d88c6",
                "667961fc-6d31-7535-bf29-857deb1feb1c",
                "e634fde8-3be5-4b90-bc79-10e43fbe5644",
                "9bbd6df8-2979-fd0c-94cd-5ba885bfb22d",
                "0fa8ac2b-ee5c-f1ba-c529-c1310d448e2d",
                "b9305126-6e1a-8ee9-d24f-56a60390fc69",
                "9baf6757-8b42-7cee-336f-c1b4260da881",
                "e854c452-15d0-3e33-06bb-e1b98e6b7e55",
                "051219ef-73c6-ee55-9d9a-9d8ee18dfa1b",
                "e39025d8-53ea-2f55-95f0-c01ce96cfe5e",
                "fb98cde9-9b0a-59b0-1cb1-293d6c414b3a",
                "8fb52bed-fd7f-4f6a-898a-fac8ab4986a4",
                "619d1eaa-9471-97b1-1123-4141fbdfa8e1",
                "1f192704-8a9d-fa00-0ec1-8db48dd8f08d",
                "37b6a317-06d4-9994-2dc1-cfc3244c18c3",
                "95f90b83-61af-166c-9550-44729e5e2a82",
                "25ab90cf-e7ad-0dff-54b1-cd8df0fda898",
                "3cdc4cab-2bab-b11b-9f21-8eb59f3aa314",
                "550c0abd-5574-84ef-48d9-96e07808890d",
                "33cc3cc3-0f05-4a69-43da-39e1f7aa41d6",
                "df71879a-db02-f522-b108-2f143a7d8310",
                "31e10d90-7a8b-ee9f-3c5f-541cc0775b02",
                "b4ea0db9-a8b5-e018-a78b-65bb7f53ee5e",
                "968c98ba-413f-65f5-6628-3f7913940882",
                "02bc67a9-0f0e-4578-8b19-8d342a375e04",
                "6851dbe0-1b76-835a-502a-3e07a6f1cd85",
                "8e23d432-f434-ca76-60a4-1258d8057904",
                "75683f06-ab6e-7b28-e64f-af432ecd416e",
                "b52aff98-179f-4d8a-6f85-dd14dc93fe01",
                "62b17fe5-1c88-88bb-b527-44969a903d2a",
                "6b6e5975-3b55-c66a-398a-076ac4711aa4",
                "d4c9d489-4cbc-3dac-2771-79776af2d359",
                "b428d9a0-ce91-73f3-9017-e107ccb4b613",
                "f24e2f85-4ef2-2018-45f1-96e513bba4b5",
                "ddf7fe16-29b5-c713-194b-e020edc17d79",
                "0a7a4cac-775a-3778-502c-20a34dc56084",
                "0960e71c-2f29-27e1-c4fe-80802dbaea6e",
                "e3ba11e9-482a-9a85-e641-850aef11a444",
                "0bd2bb52-704b-41e7-75e7-71f981eb28d6",
                "7d3f0246-7492-5ba6-6f8f-26d6cd098222",
                "b8df60b6-7f2c-3485-bd9b-dacb1a440637",
                "5923a866-7fae-9be8-0586-7b3979c4a89f",
                "9855c8ff-5e64-7285-fba4-cefcbbe0f5cd",
                "b657de72-a502-8c51-dd9b-8e4e2527269c",
                "d7fdfb3c-e308-aaa1-52f5-95d3cdf8367a",
                "686b161a-4bcb-b915-bbe5-2c8083367e9f",
                "dee3a2fb-0455-d5fa-e142-a96a2d425851",
                "dc14c8b8-e75e-04d6-cffa-e09dbd021614",
                "2c1ff041-7df2-bf40-0360-dbc8f08c22ee",
                "3c2bf93a-0a94-1d90-749b-75928e08f114",
                "26dfcd64-fafa-89da-84c3-d1ca69a8ac93",
                "d3c49eb1-fb35-b25c-662a-9c56668fe979",
                "1a414804-cf08-5751-9eff-4c257eb2d348",
                "f788aeb1-4ee4-74ef-afd9-3852e58c9e31",
                "fd81e9cd-4b00-f833-a3ba-e033d488fa8b",
                "010ce936-585d-4790-702f-dc2849fda4f6",
                "8ceb9cbb-5077-3619-c640-b872bea6ada5",
                "88bbb44b-e942-90df-1e93-d5061f6daa5d",
                "216e4d63-31e4-d0d2-cd1e-6966b6db4d04",
                "0a121437-c69d-e584-8c76-341fbe391997",
                "cb47fe82-66b2-93fe-9424-23d328280a32",
                "526c7ba5-1e2f-e707-fc21-a8664702bbee",
                "94ae3a95-f9dd-7cef-ef2d-6d9d8a52f08f",
                "16c71c99-4002-1656-0bfb-71c2add181fd",
                "e9bbab6c-21f1-3035-30bf-fa8317d721ff",
                "c12d1e92-a9ad-e388-1f12-c9456e1fd868",
                "323adc9b-7689-8a36-7881-5bde9ca99ba1",
                "74fbd9d5-8697-099c-3024-280e97b0bb98",
                "f7d17508-d606-0123-718b-98597f38f924",
                "0595288c-345d-5365-6de4-b6c40e3a69c4",
                "dd4a22bb-eb5f-22fa-02bb-59e67a637ccd",
                "89e31dd4-e9e0-e318-30ea-ba1b0888cc79",
                "c9ba790d-7fae-63b2-8d9a-bd8972ae7a2a",
                "4bfb3108-6e30-0201-d3b6-c0192fa218da",
                "a2854802-f0eb-3e71-581b-2f21b142dbae",
                "fa6b1ff4-476a-7b2d-e9e7-22b6c3b1779d",
                "0482e264-c0a1-8bcb-dbae-d2fa5f9e41eb",
                "b4fe8e13-8b64-6e90-4f15-c86ad0984293",
                "2e6ac1fa-c435-b9d7-8a99-cd5b30e4feac",
                "e1c9cc74-8f74-9dcb-ce35-c05e7e801031",
                "af825370-20d4-7758-30c7-2cc79a2cc7a1",
                "b964dd24-c670-f385-b885-6dd89c342ec1",
                "33acbad9-a161-d566-4c83-1257a1ed9de2",
                "5059355a-2b63-1aaa-f075-e70e3489790c",
                "7fba8465-4d56-0023-e55d-d8397f61d4f6",
                "ca1dc71d-6e90-645b-4bea-c58bb7bb5705",
                "d1eced21-b847-77bd-7948-3bb7c2136da6",
                "8ccfa487-7ca8-090d-14cc-10140e26d2c0",
                "2522f443-7ca6-b856-01b3-5d5592ee66e7",
                "40c32f1b-3acf-bd0f-f3a1-960aef21c742",
                "6b788337-ba3a-324a-bfbd-03a2680f1286",
                "3a7bb8bb-649d-941a-6e22-fc76974dec33",
                "569b1c78-1f20-347f-8acf-611a188f388c",
                "043131c4-defa-59ac-3323-9f6532b03161",
                "e8cc81f9-5361-b238-5c24-91fd832f59bc",
                "9a92690d-59ca-0261-0d36-ec9a4deb4b3f"
            };
            HealthRecordsRequest healthRecordsRequest = new HealthRecordsRequest
            {

                ResourceType = nameof(Observation),
                LogicalIdentifier = ids.Select(Guid.Parse)
            };
            return healthRecordsRequest;
        }

        public static HealthRecordsRequest ImagingStudyHealthRecordsRequest()
        {
            var ids = new string[]
            {
                "5a1cac61-b61c-b1c3-01d2-5857a83438eb",
                "F1E7CDB4-B48C-A858-C4CD-0906A6DD7929",
                "466FAD63-FAF0-E42F-ACFA-2B1C5D9C272D",
                "D45174E0-0E58-EE54-BE84-33C855425358",
                "418C3FA6-427C-B2C5-B1A1-59F38F482C67",
                "5F495B98-C896-A848-9C57-6B862C89EF7E",
                "BAE3627B-1E5F-23F3-5E92-6DAE624DBCC0",
                "B5AC5B2E-7B40-21B8-A8F8-7A310DF04A2D",
                "CF841EBA-38C9-0E85-EEBA-889935675298",
                "6DA1A00D-6D7F-55F2-A0A2-CE70392EF068",
            };
            HealthRecordsRequest healthRecordsRequest = new HealthRecordsRequest
            {
                ResourceType = nameof(ImagingStudy),
                LogicalIdentifier = ids.Select(Guid.Parse)
            };
            return healthRecordsRequest;
        }

        public static HealthRecordsRequest SpecimenHealthRecordsRequest()
        {
            var ids = new string[]
            {
                "FF50D2A0-E3EE-5CBE-916E-E0F5A535CDC1",
                "B3D86F8B-8FDD-C1F6-F7C4-3AC21CD66418",
                "44D79F55-F0DF-5059-D6BA-F677092C1EA0",
                "0E08FEEA-2DB7-8F02-1471-351962F3974D",
                "9B4841DE-1EF5-8B5C-9D23-E12048572186",
                "AEAEAE5B-4DB2-3BF6-A7CF-A84810B85612",
                "603D3038-4AA8-D5FD-93F3-A3A020862202",
                "5439307F-9F67-A701-651F-639FE3CF4B57",
                "CB7125AB-C516-BDF1-22B1-80141D1815A7",
                "E4A6A8C4-E7C1-D5FC-B231-4507BA3A193F"
            };
            HealthRecordsRequest healthRecordsRequest = new HealthRecordsRequest
            {
                ResourceType = nameof(Specimen),
                LogicalIdentifier = ids.Select(Guid.Parse)
            };
            return healthRecordsRequest;
        }

        public static HealthRecordsRequest ObservationHealthRecordsRequest()
        {
            var ids = new string[]
            {
                "21748C07-AA58-A460-8518-1A93F48F424F",
                "C1258861-497D-D89B-C833-17E5CBB246F5",
                "73B70122-B0F9-5EF9-C55F-9855D0F0AB99",
                "7F509AFB-6E6A-E11F-6250-4AAEAB51A1BF",
                "5C0D29A3-7D67-7441-8901-CEC65AFAD2AE",
                "D2112447-4D52-AD41-7D6A-A9AB63CE5EA9",
                "54F4B253-C2E2-EF81-AFDA-2A9919411E59",
                "429FFBD4-0059-3DAD-468A-ABB56C82FF85",
                "692537FB-C2E8-1FE1-BCB5-BC81A80E18A6",
                "F20005DB-3C5D-817A-6AFE-3DE73488EE9E"
            };
            HealthRecordsRequest healthRecordsRequest = new HealthRecordsRequest
            {
                ResourceType = nameof(Observation),
                LogicalIdentifier = ids.Select(Guid.Parse)
            };
            return healthRecordsRequest;
        }
    }
}