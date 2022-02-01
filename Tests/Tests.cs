using NUnit.Framework;
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
        public void Test_When_Include_adding_one_parameter_when_condition_true()
        {
            var requestUrl =
                RequestUrl("")
                    .When(1 < 2).Include("numbers", "123")
                    .ToString();

            Assert.AreEqual("?numbers=123", requestUrl);
        }

        [Test]
        public void Test_When_Include_adding_one_parameter_when_condition_false()
        {
            var requestUrl =
                RequestUrl("")
                    .When(1 > 2).Include("numbers", "123")
                    .ToString();

            Assert.AreEqual("", requestUrl);
        }

        [Test]
        public void Test_When_Include_adding_two_parameters_when_both_true()
        {
            var requestUrl =
                RequestUrl("")
                    .When(1 < 2).Include("numbers", "123")
                    .When(3 < 4).Include("letters", "ABC")
                    .ToString();

            Assert.AreEqual("?numbers=123&letters=ABC", requestUrl);
        }

        [Test]
        public void Test_WhenTrueInclude_adding_two_parameters_when_first_false()
        {
            var requestUrl =
                RequestUrl("")
                    .When(1 > 2).Include("numbers", "123")
                    .When(3 < 4).Include("letters", "ABC")
                    .ToString();

            Assert.AreEqual("?letters=ABC", requestUrl);
        }

        [Test]
        public void Test_WhenTrueInclude_adding_two_with_lambda_function()
        {
            var lettersString = "ABC";

            var requestUrl =
                RequestUrl("")
                    .When(1 < 2).Include("numbers", "123")
                    .When(3 < 4).Include("letters", () => { return lettersString.ToLower(); })
                    .ToString();

            Assert.AreEqual("?numbers=123&letters=abc", requestUrl);
        }

        [Test]
        public void Test_WhenTrueInclude_adding_two_parameters_with_a_non_empty_path_part()
        {
            var requestUrl =
                RequestUrl("/v1/something")
                    .When(1 < 2).Include("numbers", "123")
                    .When(3 < 4).Include("letters", "ABC")
                    .ToString();

            Assert.AreEqual("/v1/something?numbers=123&letters=ABC", requestUrl);
        }

        [Test]
        public void Test_With_where_we_always_add_one_parameter()
        {
            var requestUrl =
                RequestUrl("/v1/something")
                    .With("numbers", "123")
                    .ToString();

            Assert.AreEqual("/v1/something?numbers=123", requestUrl);
        }

        [Test]
        public void Test_With_where_we_always_add_two_parameters()
        {
            var requestUrl =
                RequestUrl("/v1/something")
                    .With("numbers", "123")
                    .With("letters", "ABC")
                    .ToString();

            Assert.AreEqual("/v1/something?numbers=123&letters=ABC", requestUrl);
        }

        [Test]
        public void Test_MaybeWith_adding_nullable_integer_that_is_null()
        {
            int? getNullInt()
            {
                return null;
            }

            var requestUrl =
                RequestUrl("/v1/something")
                    .MaybeWith("integer", getNullInt())  // TODO: analogous needed for automatic .ToString()
                    .ToString();

            Assert.AreEqual("/v1/something", requestUrl);
        }

        [Test]
        public void Test_MaybeWith_adding_nullable_integer_that_is_12345()
        {
            int? getInt()
            {
                return 12345;
            }

            var requestUrl =
                RequestUrl("/v1/something")
                    .MaybeWith("integer", getInt())
                    .ToString();

            Assert.AreEqual("/v1/something?integer=12345", requestUrl);
        }

        [Test]
        public void Test_With_adding_null_string_throws_exception()
        {
            Assert.Throws<System.ArgumentNullException>(() => 
            {
                string nullString = null;

                RequestUrl("/v1/something")
                    .With("string", nullString)
                    .ToString();
            });
        }


        /* Example which cannot compile in this context, but gives a flavour of what we might get to.

        [Test]
        /// <inheritdoc/>
        public async Task<Response<IEnumerable<Account>>> GetLinkedAccountsAsync(OrganisationClient client, Guid? userId, string userEmail, string platform = null, int? page = null, int? itemsPerPage = null)
        {
            var requestHeaders = RequestHelper.BuildDefaultRequestHeaders();

            var requestUrl =
                RequestUrl($"/v1/accounts/12345/product-instances")
                    .MaybeWith("businessId", businessId)
                    .MaybeWith("productLicenseId", productLicenseId)
                    .MaybeWith("productType", productType)
                    .MaybeWith("page", page)
                    .MaybeWith("page-items", itemsPerPage)
                    .When(includeSoftDeleted).Include("includeSoftDeleted", "true")
                    .ToString();

            var response = await client.GetAsync(requestUrl, requestHeaders);
            return await response.ConvertToResponse<IEnumerable<ProductInstance>>();
        }

        */
    }
}