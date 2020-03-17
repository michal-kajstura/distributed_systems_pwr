package rsi;

import org.apache.xmlrpc.XmlRpcClient;

import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;
import java.util.Vector;

public class Main {

    public static void main(String[] args) {
        try {
            XmlRpcClient srv = new XmlRpcClient("http://localhost:6006");
            var scanner = new Scanner(System.in);

            Vector<Integer> params = new Vector<>();
            Object result = srv.execute("MojSerwer.show", params);
            var methodList = (List<String>) result;


            while(true) {
                for (int i=0; i < methodList.size(); i++) {
                    System.out.println(String.valueOf(i) + ". " + methodList.get(i));
                }
                System.out.print("Podaj indeks: ");
                var chosenIndex = scanner.nextInt();
                var chosenMethod = methodList.get(chosenIndex);
                var method = parseMethod(chosenMethod);
                System.out.println(method);

                scanner = new Scanner(System.in);
                var methodParams = new Vector<>();
                for (var t : method.paramTypes) {
                    System.out.print("Podaj parametr (" + t + "): ");
                    var paramStr = scanner.nextLine();

                    Object castedParam = null;
                    if (t.equals("String")) {
                        castedParam = paramStr;
                    } else {
                        var absoluteType = "java.lang." + t;
                        var klass = Class.forName(absoluteType);
                        var classConverter = klass.getMethod("valueOf", String.class);
                        castedParam = classConverter.invoke(klass, paramStr);
                    }
                    methodParams.addElement(castedParam);
                }

                result = srv.execute("MojSerwer." + method.name, methodParams);
                System.out.println(result);
            }

        } catch (Exception exception) {
            System.err.println("Klient XML_RPC: " + exception);
        }
    }

    private static Method parseMethod(String toParse) {
        var tokens = toParse.split(";");
        var paramTypes = new ArrayList<String>();
        for (int i = 1; i < tokens.length - 1; i++)
            paramTypes.add(tokens[i]);
        return new Method(tokens[0], paramTypes, tokens[tokens.length - 1]);
    }
}

class Method {
    public String name;
    public String description;
    public List<String> paramTypes;

    public Method(String name, List<String> paramTypes, String description) {
        this.name = name;
        this.paramTypes = paramTypes;
        this.description = description;
    }

    @Override
    public String toString() {
        return "Nazwa: " + name
                + ", typy parametrów: " + paramTypes
                + ", opis: " + description;
    }
}
