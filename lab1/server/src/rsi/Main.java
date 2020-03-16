package rsi;

import org.apache.xmlrpc.WebServer;

public class Main {

    public static void main(String[] args) {
        try {
            System.out.println("Startuję serwer XML-RPC");
            var server = new WebServer(6006);
            server.addHandler("MojSerwer", new Server());
            server.start();
            System.out.println("Serwer wystartował pomyślnie");
            System.out.println("Aby zatrzymać serwer nacisnij ctrl+c");
        } catch (Exception exception) {
            System.err.println("Serwer XML-RPC:" + exception);
        }
    }
}
