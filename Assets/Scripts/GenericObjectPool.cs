using System.Collections.Generic;
using UnityEngine;

public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component
{
    // A generic approach to object pooling - Slightly better than the dictionary based gameobject approach

    [SerializeField] T _prefab;

    Queue<T> _objectsQueue = new Queue<T>();

    public static GenericObjectPool<T> Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public T Get()
    {
        if (_objectsQueue.Count == 0)
        {
            AddObjects(1);
        }

        return _objectsQueue.Dequeue();
    }

    void AddObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            T newObject = Instantiate(_prefab);
            newObject.gameObject.SetActive(false);
            _objectsQueue.Enqueue(newObject);
        }
    }

    public void ReturnToPool(T objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        _objectsQueue.Enqueue(objectToReturn);
    }

}
