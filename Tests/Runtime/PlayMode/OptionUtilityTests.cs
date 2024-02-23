using NUnit.Framework;
using UnityEngine;
using static SafedGames.Options.OptionUtilities;

namespace SafedGames.Options.Tests.Runtime.PlayMode
{
    public sealed class OptionUtilityTests
    {
        private GameObject _rootGameObject;

        [SetUp]
        public void Setup()
        {
            Debug.unityLogger.logEnabled = false;
            _rootGameObject = new GameObject("Root");
        }

        [TearDown]
        public void TearDown()
        {
            Debug.unityLogger.logEnabled = true;
            Object.DestroyImmediate(_rootGameObject);
        }

        [Test]
        public void FindGameObject_ObjectExists_ReturnsSome()
        {
            const string name = "testName";
            _ = new GameObject(name)
            {
                transform =
                {
                    parent = _rootGameObject.transform
                }
            };

            Assert.True(FindGameObject(name).IsSome);
        }

        [Test]
        public void FindGameObject_MultipleObjectsExists_ReturnsCorrectObject()
        {
            _ = new GameObject("Invalid")
            {
                transform =
                {
                    parent = _rootGameObject.transform
                }
            };

            const string name = "testName";
            _ = new GameObject(name)
            {
                transform =
                {
                    parent = _rootGameObject.transform
                }
            };

            var result = FindGameObject(name);
            Assert.True(result.IsSome);
            Assert.That(result.OrThrow().name, Is.EqualTo(name));
        }

        [Test]
        public void FindGameObject_ObjectDoesNotExist_ReturnsNone()
        {
            const string name = "testName";
            const string existingName = "failName";
            _ = new GameObject(existingName)
            {
                transform =
                {
                    parent = _rootGameObject.transform
                }
            };

            Assert.True(FindGameObject(name).IsNone);
        }

        [Test]
        public void FindGameObjectWithTag_ObjectExists_ReturnsSome()
        {
            _ = new GameObject
            {
                tag = "EditorOnly",
                transform =
                {
                    parent = _rootGameObject.transform
                }
            };

            Assert.True(FindGameObjectWithTag("EditorOnly").IsSome);
        }

        [Test]
        public void FindGameObjectWithTag_ObjectDoesNotExist_ReturnsNone()
        {
            _ = new GameObject
            {
                transform =
                {
                    parent = _rootGameObject.transform
                }
            };

            Assert.True(FindGameObjectWithTag("EditorOnly").IsNone);
        }

        [Test]
        public void FindGameObjectWithTag_InvalidTag_ReturnsNone()
        {
            const string tag = "AN UnUsEd TaG";
            _ = new GameObject
            {
                transform =
                {
                    parent = _rootGameObject.transform
                }
            };

            Assert.True(FindGameObjectWithTag(tag).IsNone);
        }

        [Test]
        public void FindGameObjectsWithTag_ObjectExists_ReturnsSome()
        {
            _ = new GameObject
            {
                tag = "EditorOnly",
                transform =
                {
                    parent = _rootGameObject.transform
                }
            };

            Assert.True(FindGameObjectsWithTag("EditorOnly").IsSome);
        }

        [Test]
        public void FindGameObjectsWithTag_ManyObjectsExist_ReturnsSomeOfCorrectSize()
        {
            const int numberOfObjects = 10;
            for (var createIndex = 0; createIndex < numberOfObjects; ++createIndex)
            {
                _ = new GameObject
                {
                    tag = "EditorOnly",
                    transform =
                    {
                        parent = _rootGameObject.transform
                    }
                };
            }

            var result = FindGameObjectsWithTag("EditorOnly");
            Assert.True(result.IsSome);
            Assert.That(result.OrThrow().Length, Is.EqualTo(numberOfObjects));
        }

        [Test]
        public void FindGameObjectsWithTag_ManyObjectsExist_WithMixedTags_ReturnsSomeOfCorrectSize()
        {
            const int numberOfObjects = 10;
            for (var createIndex = 0; createIndex < numberOfObjects; ++createIndex)
            {
                _ = new GameObject
                {
                    tag = "EditorOnly",
                    transform =
                    {
                        parent = _rootGameObject.transform
                    }
                };
            }

            for (var createIndex = 0; createIndex < numberOfObjects; ++createIndex)
            {
                _ = new GameObject
                {
                    transform =
                    {
                        parent = _rootGameObject.transform
                    }
                };
            }

            var result = FindGameObjectsWithTag("EditorOnly");
            Assert.True(result.IsSome);
            Assert.That(result.OrThrow().Length, Is.EqualTo(numberOfObjects));
        }

        [Test]
        public void FindGameObjectsWithTag_ObjectDoesNotExist_ReturnsNone()
        {
            var gameObjectUnderTest = new GameObject();
            gameObjectUnderTest.transform.parent = _rootGameObject.transform;

            Assert.True(FindGameObjectsWithTag("EditorOnly").IsNone);
        }

        [Test]
        public void FindGameObjectsWithTag_InvalidTag_ReturnsNone()
        {
            const string tag = "AN UnUsEd TaG";
            _ = new GameObject
            {
                transform =
                {
                    parent = _rootGameObject.transform
                }
            };

            Assert.True(FindGameObjectsWithTag(tag).IsNone);
        }

        [Test]
        public void FindMainCamera_CameraExists_ReturnsSome()
        {
            const string tag = "MainCamera";
            var camera = new GameObject
            {
                tag = tag,
                transform =
                {
                    parent = _rootGameObject.transform
                }
            };

            camera.AddComponent<Camera>();

            Assert.True(FindMainCamera().IsSome);
        }

        [Test]
        public void FindMainCamera_CameraExists_ReturnsSomeOfCorrectCamera()
        {
            const string tag = "MainCamera";
            var camera = new GameObject
            {
                tag = tag,
                transform =
                {
                    parent = _rootGameObject.transform
                }
            }.AddComponent<Camera>();

            Assert.That(FindMainCamera().OrThrow(), Is.EqualTo(camera));
        }

        [Test]
        public void FindMainCamera_CameraDoesNotExist_ReturnsNone()
        {
            Assert.True(FindMainCamera().IsNone);
        }

        [Test]
        public void FindMainCamera_CameraIsNotTagged_ReturnsNone()
        {
            _ = new GameObject
            {
                transform =
                {
                    parent = _rootGameObject.transform
                }
            }.AddComponent<Camera>();

            Assert.True(FindMainCamera().IsNone);
        }

        [Test]
        public void FindMainCamera_ObjectTaggedWithoutCamera_ReturnsNone()
        {
            const string tag = "MainCamera";
            _ = new GameObject
            {
                tag = tag,
                transform =
                {
                    parent = _rootGameObject.transform
                }
            };

            Assert.True(FindMainCamera().IsNone);
        }

        [Test]
        public void FindComponent_HasCorrectComponent_ReturnsSome()
        {
            var container = new GameObject
            {
                transform =
                {
                    parent = _rootGameObject.transform
                }
            };

            container.AddComponent<Camera>();

            Assert.True(container.FindComponent<Camera>().IsSome);
        }

        [Test]
        public void FindComponent_HasCorrectComponent_ReturnsSomeOfCOrrectValue()
        {
            var container = new GameObject
            {
                transform =
                {
                    parent = _rootGameObject.transform
                }
            };

            var camera = container.AddComponent<Camera>();

            Assert.That(container.FindComponent<Camera>().OrThrow(), Is.EqualTo(camera));
        }

        [Test]
        public void FindComponent_DoesNotHaveCorrectComponent_ReturnsNone()
        {
            var container = new GameObject
            {
                transform =
                {
                    parent = _rootGameObject.transform
                }
            };

            container.AddComponent<SpriteRenderer>();

            Assert.True(container.FindComponent<Camera>().IsNone);
        }
    }
}
