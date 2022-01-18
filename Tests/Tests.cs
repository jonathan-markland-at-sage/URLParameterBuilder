using NUnit.Framework;
using ApiQueryParameterBuilder;

namespace Tests
{
    public class Tests
    {
        [Test]
        public void Test_adding_nothing()
        {
            var builder = new ApiQueryParamBuilder();
            var result = builder.ToString();
            Assert.AreEqual("", result);
        }

        [Test]
        public void Test_adding_one_parameter_when_condition_true()
        {
            var builder =
                new ApiQueryParamBuilder()
                    .If(1 < 2).Add("numbers", "123");

            var result = builder.ToString();
            Assert.AreEqual("?numbers=123", result);
        }

        [Test]
        public void Test_adding_one_parameter_when_condition_false()
        {
            var builder =
                new ApiQueryParamBuilder()
                    .If(1 > 2).Add("numbers", "123");

            var result = builder.ToString();
            Assert.AreEqual("", result);
        }

        [Test]
        public void Test_adding_two_parameters_when_both_true()
        {
            var builder =
                new ApiQueryParamBuilder()
                    .If(1 < 2).Add("numbers", "123")
                    .If(3 < 4).Add("letters", "ABC");

            var result = builder.ToString();
            Assert.AreEqual("?numbers=123&letters=ABC", result);
        }

        [Test]
        public void Test_adding_two_parameters_when_first_false()
        {
            var builder =
                new ApiQueryParamBuilder()
                    .If(1 > 2).Add("numbers", "123")
                    .If(3 < 4).Add("letters", "ABC");

            var result = builder.ToString();
            Assert.AreEqual("?letters=ABC", result);
        }

        [Test]
        public void Test_adding_two_with_lambda_function()
        {
            var lettersString = "ABC";

            var builder =
                new ApiQueryParamBuilder()
                    .If(1 < 2).Add("numbers", "123")
                    .If(3 < 4).Add("letters", () => { return lettersString.ToLower(); });

            var result = builder.ToString();
            Assert.AreEqual("?numbers=123&letters=abc", result);
        }

        /*
        [Test]
        public void Test_example()
        {
            var requestHeaders = RequestHelper.BuildDefaultRequestHeaders();

            var requestUrl =
                new ApiQueryParamBuilder($"/v1/accounts/{accountId}/product-instances")
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