#### Before use echo instances you should connect to node.
You need do this once, before start, and after force disconnect (if you want to continue work).
Lib provide reconnecting socket it can reconnect automatically after network disconnect or node disconnect, you can tune **ConnectionManager.tryConnectCount** to set maximum count of retries and **ConnectionManager.delayBetweenTryConnect** to set delay betweeen reconnection attempts.
