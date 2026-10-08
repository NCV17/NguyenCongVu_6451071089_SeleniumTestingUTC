# Performance Testing

## TC08 - Response Time

- **Test Case**: TC08 - Kiểm tra Response Time
- **Target**: `https://vanphongdientu.utc.edu.vn/Login`
- **Request**: HTTP GET request to Login page
- **Test data**: None (no credentials needed for simple page load)
- **Metric**: Response Time (ms)
- **Expected result**: Page loads within acceptable threshold.
- **Threshold**: < 2000 ms

_This test is run using JMeter via the `Performance/TC08_ResponseTime.jmx` plan._

## TC09 - Load Testing

- **Test Case**: TC09 - Load Testing
- **Number of users**: 50, 100 users
- **Ramp-up**: 30 seconds
- **Duration**: 300 seconds (5 minutes)
- **Target**: `https://vanphongdientu.utc.edu.vn/Login`
- **Request**: HTTP GET request to Login page
- **Response time**: TBD (Depends on test result)
- **Throughput**: TBD
- **Error rate**: TBD (< 1% Expected)

_This test is run using JMeter via the `Performance/TC09_LoadTesting.jmx` plan._
