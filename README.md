# C#-Json-parser
This will take JSON source and parse it to a C# object/class.

//Moved the repository

<details><summary>Goals</summary>
<p>

| Todo                                                                                      |
|-------------------------------------------------------------------------------------------|
| Clean up source :x:                                                                        |
| Optimize source :x:                                                                        |

- [x] Supports blocks
- [x] Supports assignment
- [x] Supports strings, numbers and booleans
- [x] Supports nesting blocks, etc. 

- [x] Faster than Newtonsoft
- [ ] Same amount of features as Newtonsoft
  
  </p>
</details>
<details><summary>Change Logs</summary>
<p>

| Change Logs |
|-------------|
| Added bools |
| Added array things [] |
| Added node indexing refer to example 1 |
| Cleaned up node classes |

Example 1:

````C#
node[0]["Prices"]["Oranges"]["Price"]; //would return the price
````
 
</details>

<details><summary>Comparing Newtonsoft</summary>
<p>

![image](https://user-images.githubusercontent.com/74394136/150625130-83512275-f03f-4885-a535-93a0a62efff3.png)

I tested 5 times and it is indeed faster than newtonsoft, so that's cool I guess!

Mine:

![image](https://user-images.githubusercontent.com/74394136/150625243-49f2c306-214b-411a-878d-663b702b98bf.png)


Newtonsoft:

![image](https://user-images.githubusercontent.com/74394136/150625260-a419b7a5-5cff-43fc-8d72-1b3ae5c15361.png)
    </p>
</details>
