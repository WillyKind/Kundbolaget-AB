using System.Collections.Generic;
using Kundbolaget.JsonEntityModels;

namespace Tests
{
    public static class Orders
    {
        public static OrderFile OrderNewCoop => new OrderFile
        {
            companyId = 2,
            customerOrderFileId = 54,
            orders = new List<SubOrder>
            {
                new SubOrder
                {
                    deliverTo = 7,
                    deliverDate = "2017-03-02",
                    orderedProducts = new List<OrderRow>
                    {
                        new OrderRow
                        {
                            productId = 6,
                            amount = 5
                        },
                        new OrderRow
                        {
                            productId = 7,
                            amount = 1
                        },
                        new OrderRow
                        {
                            productId = 3,
                            amount = 5
                        }
                    }
                }
            }
        };

        public static OrderFile OrderCoop => new OrderFile
        {
            companyId = 2,
            customerOrderFileId = 53,
            orders = new List<SubOrder>
            {
                new SubOrder
                {
                    deliverTo = 5,
                    deliverDate = "2017-03-02",
                    orderedProducts = new List<OrderRow>
                    {
                        new OrderRow
                        {
                            productId = 1,
                            amount = 10
                        },
                        new OrderRow
                        {
                            productId = 4,
                            amount = 5
                        }
                    }
                },
                new SubOrder
                {
                    deliverTo = 4,
                    deliverDate = "2017-03-02",
                    orderedProducts = new List<OrderRow>
                    {
                        new OrderRow
                        {
                            productId = 6,
                            amount = 5
                        },
                        new OrderRow
                        {
                            productId = 7,
                            amount = 1
                        }
                    }
                }
            }
        };

        public static OrderFile OrderIcaWrongMotherCompanyId => new OrderFile
        {
            companyId = 99999,
            customerOrderFileId = 53,
            orders = new List<SubOrder>
            {
                new SubOrder
                {
                    deliverTo = 3,
                    deliverDate = "2017-03-02",
                    orderedProducts = new List<OrderRow>
                    {
                        new OrderRow
                        {
                            productId = 6,
                            amount = 12
                        },
                        new OrderRow
                        {
                            productId = 2,
                            amount = 30
                        }
                    }
                },
                new SubOrder
                {
                    deliverTo = 6,
                    deliverDate = "2017-03-02",
                    orderedProducts = new List<OrderRow>
                    {
                        new OrderRow
                        {
                            productId = 2,
                            amount = 50
                        },
                        new OrderRow
                        {
                            productId = 4,
                            amount = 1
                        }
                    }
                }
            }
        };

        public static OrderFile OrderIca => new OrderFile
        {
            companyId = 1,
            customerOrderFileId = 53,
            orders = new List<SubOrder>
            {
                new SubOrder
                {
                    deliverTo = 3,
                    deliverDate = "2017-03-02",
                    orderedProducts = new List<OrderRow>
                    {
                        new OrderRow
                        {
                            productId = 6,
                            amount = 12
                        },
                        new OrderRow
                        {
                            productId = 2,
                            amount = 30
                        }
                    }
                },
                new SubOrder
                {
                    deliverTo = 6,
                    deliverDate = "2017-03-02",
                    orderedProducts = new List<OrderRow>
                    {
                        new OrderRow
                        {
                            productId = 2,
                            amount = 50
                        },
                        new OrderRow
                        {
                            productId = 4,
                            amount = 1
                        }
                    }
                }
            }
        };


        public static OrderFile OrderIcaWrongDate => new OrderFile
        {
            companyId = 1,
            customerOrderFileId = 53,
            orders = new List<SubOrder>
            {
                new SubOrder
                {
                    deliverTo = 3,
                    deliverDate = "2017-03-02",
                    orderedProducts = new List<OrderRow>
                    {
                        new OrderRow
                        {
                            productId = 6,
                            amount = 12
                        },
                        new OrderRow
                        {
                            productId = 2,
                            amount = 30
                        }
                    }
                },
                new SubOrder
                {
                    deliverTo = 6,
                    deliverDate = "2017-01-02",
                    orderedProducts = new List<OrderRow>
                    {
                        new OrderRow
                        {
                            productId = 2,
                            amount = 50
                        },
                        new OrderRow
                        {
                            productId = 4,
                            amount = 1
                        }
                    }
                }
            }
        };

        public static OrderFile OrderIcaWrongCompanyId => new OrderFile
        {
            companyId = 1,
            customerOrderFileId = 53,
            orders = new List<SubOrder>
            {
                new SubOrder
                {
                    deliverTo = 3,
                    deliverDate = "2017-03-02",
                    orderedProducts = new List<OrderRow>
                    {
                        new OrderRow
                        {
                            productId = 6,
                            amount = 12
                        },
                        new OrderRow
                        {
                            productId = 2,
                            amount = 30
                        }
                    }
                },
                new SubOrder
                {
                    deliverTo = 99,
                    deliverDate = "2017-03-02",
                    orderedProducts = new List<OrderRow>
                    {
                        new OrderRow
                        {
                            productId = 2,
                            amount = 50
                        },
                        new OrderRow
                        {
                            productId = 4,
                            amount = 1
                        }
                    }
                }
            }
        };

        public static OrderFile IcaNegativeOrderAmmount => new OrderFile
        {
            companyId = 1,
            customerOrderFileId = 53,
            orders = new List<SubOrder>
            {
                new SubOrder
                {
                    deliverTo = 3,
                    deliverDate = "2017-03-02",
                    orderedProducts = new List<OrderRow>
                    {
                        new OrderRow
                        {
                            productId = 6,
                            amount = 12
                        },
                        new OrderRow
                        {
                            productId = 2,
                            amount = -30
                        }
                    }
                },
                new SubOrder
                {
                    deliverTo = 6,
                    deliverDate = "2017-03-02",
                    orderedProducts = new List<OrderRow>
                    {
                        new OrderRow
                        {
                            productId = 2,
                            amount = 50
                        },
                        new OrderRow
                        {
                            productId = 4,
                            amount = -1
                        }
                    }
                }
            }
        };

        public static OrderFile OrderIcaWithANoneExistingProduct => new OrderFile
        {
            companyId = 1,
            customerOrderFileId = 53,
            orders = new List<SubOrder>
            {
                new SubOrder
                {
                    deliverTo = 3,
                    deliverDate = "2017-03-02",
                    orderedProducts = new List<OrderRow>
                    {
                        new OrderRow
                        {
                            productId = 2,
                            amount = 12
                        },
                        new OrderRow
                        {
                            productId = 2,
                            amount = 30
                        }
                    }
                },
                new SubOrder
                {
                    deliverTo = 6,
                    deliverDate = "2017-03-02",
                    orderedProducts = new List<OrderRow>
                    {
                        new OrderRow
                        {
                            productId = 99,
                            amount = 50
                        },
                        new OrderRow
                        {
                            productId = 4,
                            amount = 1
                        }
                    }
                }
            }
        };
    }
}