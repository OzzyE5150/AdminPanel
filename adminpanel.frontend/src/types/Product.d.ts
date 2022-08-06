export interface IProduct {
    id: number;
    name: string;
    description: string;
    price: number;
    createdOn: Date;
    updatedOn: Date;
    isTaxable: boolean;
    isArchived: boolean;
}

export interface IProductInventory {
    id: number;
    product: IProduct;
    quantityOnHand: number;
    idealQuantiy: number;
}