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
