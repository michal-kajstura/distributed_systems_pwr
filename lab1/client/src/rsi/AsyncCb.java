package rsi;


import org.apache.xmlrpc.AsyncCallback;

import java.net.URL;

public class AsyncCb implements AsyncCallback {

    @Override
    public void handleResult(Object result, URL url, String method) {
        int res = (int) result;
        System.out.println("AsyncClass handleResult: " + String.valueOf(res));

    }

    @Override
    public void handleError(Exception e, URL url, String method) {
        System.out.println("AsyncClass handleError");
    }

}
