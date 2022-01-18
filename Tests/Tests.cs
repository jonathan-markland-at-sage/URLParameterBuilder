using NUnit.Framework;
using ApiQueryParameterBuilder;
using static ApiQueryParameterBuilder.RequestUrlModule;

namespace Tests
{
    public class Tests
    {
        [Test]
        public void Test_adding_nothing()
        {
            var requestUrl = RequestUrl("").ToString();
            Assert.AreEqual("", requestUrl);
        }

        [Test]
        public void Test_adding_one_parameter_when_condition_true()
        {
            var requestUrl =
                RequestUrl("")
                    .If(1 < 2).Add("numbers", "123")
                    .ToString();

            Assert.AreEqual("?numbers=123", requestUrl);
        }

        [Test]
        public void Test_adding_one_parameter_when_condition_false()
        {
            var requestUrl =
                RequestUrl("")
                    .If(1 > 2).Add("numbers", "123")
                    .ToString();

            Assert.AreEqual("", requestUrl);
        }

        [Test]
        public void Test_adding_two_parameters_when_both_true()
        {
            var requestUrl =
                RequestUrl("")
                    .If(1 < 2).Add("numbers", "123")
                    .If(3 < 4).Add("letters", "ABC")
                    .ToString();

            Assert.AreEqual("?numbers=123&letters=ABC", requestUrl);
        }

        [Test]
        public void Test_adding_two_parameters_when_first_false()
        {
            var requestUrl =
                RequestUrl("")
                    .If(1 > 2).Add("numbers", "123")
                    .If(3 < 4).Add("letters", "ABC")
                    .ToString();

            Assert.AreEqual("?letters=ABC", requestUrl);
        }

        [Test]
        public void Test_adding_two_with_lambda_function()
        {
            var lettersString = "ABC";

            var requestUrl =
                RequestUrl("")
                    .If(1 < 2).Add("numbers", "123")
                    .If(3 < 4).Add("letters", () => { return lettersString.ToLower(); })
                    .ToString();

            Assert.AreEqual("?numbers=123&letters=abc", requestUrl);
        }

        [Test]
        public void Test_adding_two_parameters_with_a_non_empty_path_part()
        {
            var requestUrl =
                RequestUrl("/v1/something")
                    .If(1 < 2).Add("numbers", "123")
                    .If(3 < 4).Add("letters", "ABC")
                    .ToString();

            Assert.AreEqual("/v1/something?numbers=123&letters=ABC", requestUrl);
        }

        /*
        [Test]
        public void Test_example()
        {
            var requestHeaders = RequestHelper.BuildDefaultRequestHeaders();

            var requestUrl =
                RequestUrl($"/v1/accounts/{accountId}/product-instances")
                    .If(businessId != null).Add("businessId", businessId)
                    .If(productLicenseId != null).Add("productLicenseId", productLicenseId)
                    .If(productType != null).Add("productType", productType)
                    .If(page != null).Add("page", page)
                    .If(itemsPerPage != null).Add("page-items", itemsPerPage)
                    .If(includeSoftDeleted).Add("includeSoftDeleted", "true")
                    .ToString();

            var response = await client.GetAsync(requestUrl, requestHeaders);
            return await response.ConvertToResponse<IEnumerable<ProductInstance>>();
        }
        */
    }
}