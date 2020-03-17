package rsi;

import org.apache.xmlrpc.XmlRpcClient;

import java.lang.reflect.InvocationTargetException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;
import java.util.Vector;
import java.util.stream.Collectors;

public class Main {

    public static void main(String[] args) {
        try {
            XmlRpcClient srv = new XmlRpcClient("http://localhost:6006");

            Vector<Integer> params = new Vector<>();
            Object result = srv.execute("MojSerwer.show", params);
            var methodStrList = (List<String>) result;
            var methodList = methodStrList.stream()
                    .map(Main::parseMethod)
                    .collect(Collectors.toList());

            while (true) {
                showMethods(methodList);
                Method method = chooseMethod(methodList);

                if (method == null)
                    break;
                Vector<Object> methodParams = getParams(method);

                if (method.asyn) {
                    var callback = new AsyncCb();
                    srv.executeAsync("MojSerwer." + method.name, methodParams, callback);
                } else {
                    result = srv.execute("MojSerwer." + method.name, methodParams);
                    System.out.println("Wynik to: " + result);
                    System.out.println("\n");
                }
            }

            System.out.println("Koniec programu");

        } catch (Exception exception) {
            System.err.println("Klient XML_RPC: " + exception);
        }
    }

    private static void showMethods(List<Method> methodList) {
        for (int i = 0; i < methodList.size(); i++) {
            System.out.println(String.valueOf(i) + ". " + methodList.get(i));
        }
        System.out.println(String.valueOf(methodList.size()) + ". Koniec");
    }

    private static Vector<Object> getParams(Method method) throws ClassNotFoundException, NoSuchMethodException, IllegalAccessException, InvocationTargetException {
        var scanner = new Scanner(System.in);
        var methodParams = new Vector<>();
        for (var t: method.paramTypes) {
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
        return methodParams;
    }

    private static Method chooseMethod(List<Method> methodList) {
        var scanner = new Scanner(System.in);
        System.out.print("Podaj number metody: ");

        var chosenIndex = scanner.nextInt();
        if (chosenIndex >= methodList.size())
            return null;

        var method = methodList.get(chosenIndex);
        System.out.println(method);
        return method;
    }

    private static Method parseMethod(String toParse) {
        var tokens = toParse.split(";");
        var paramTypes = new ArrayList<String>();
        for (int i = 1; i < tokens.length - 2; i++)
            paramTypes.add(tokens[i]);
        var asyn = tokens[tokens.length - 1].equals("true");
        return new Method(tokens[0], paramTypes, tokens[tokens.length - 2], asyn);
    }
}

class Method {
    public String name;
    public String description;
    public List<String> paramTypes;
    public boolean asyn;

    public Method(String name, List<String> paramTypes, String description, boolean asyn) {
        this.name = name;
        this.paramTypes = paramTypes;
        this.description = description;
        this.asyn = asyn;
    }

    @Override
    public String toString() {
        return "Nazwa: " + name
                + ", typy parametrów: " + paramTypes
                + ", opis: " + description;
    }
}
