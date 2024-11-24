# Batch
## about_DataverseBatch

# SHORT DESCRIPTION
Describes the use of Batches with Dataverse Requests.

# LONG DESCRIPTION
Dataverse supports the execution of requests in batches. When executing multiple requests this can result in a significant performance improvement.

> _NOTE:_
> Batches have a limit of a maximum of 1.000 request per batch. The module does NOT check on this limit, and does NOT handle to paging of batches with more than a 1.000 request. This is the responsibility of the caller.

To use batches first initialize a batch using `Request-DataverseBatch`. This returns a BatchId.
The BatchId can be provide to other requests through the `-Batch` parameter. When provided the request isn't executed, but added to the provided batch.
To execute the request collected in a batch call `Submit-DataverseBatch` and provide the BatchId. This executes all requests in the Batch, returns the results (Response or Fault), and cleans up the Batch. After submitting a batch the batch is no longer available.

# SEE ALSO
[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/about_DataverseBatch.md)
[Request-DataverseBatch](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Request-DataverseBatch.md)
[Submit-DataverseBatch](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Submit-DataverseBatch.md)
