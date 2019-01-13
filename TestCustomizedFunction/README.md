# CustomFunctionBuilder

## Overview

### Why Use `CustomFunctionBuilder`?

- In some cases, it can be arduous to create a function in runtime, even with the assistance of _lambda expression_ and pre-defined delegate such as `Func<T>` or `Action<T>`. 
- Think about the following circumstance: Suppose you have to define a delegate in a module, but you only know that it takes two parameters, while it will be passed to the next module which will define the expression mainbody. 

    It could cause some difficulties in decoupling.  

- Hence the class `CustomFunctionBuilder` is introduced to overcome this. 

### Implementation

- An instantiated object of type `CustomFunctionBuilder` is consisted of emulated local variables and emulated statements. 

- Statements, including functions and methods acquire arguments directly from the emulated local variable pool. In other words, all functions inside `CustomFunctionBuilder` statement pool take no arguments. 

## Documentation

- tempVariable

    This list is used for storing emulated local variables. 

    ``` C#
    private Dictionary<string, object> tempVariables
    ```

- executionSequence

    ``` C#
    private Dictionary<string, object> executionSequence
    ```

- Constructor

- AddVariable

    ```C#
    public void AddVariable(string name, object data)
    ```

- AddFunction

- Invoke

- Indexer

- GetTempVariable

- SetTempVariable

- ShowInvocationOrder