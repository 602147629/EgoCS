﻿using UnityEngine;
using System.Collections.Generic;

public class EgoParentConstraint<C1, CS1> : EgoParentConstraint
    where C1 : Component
    where CS1 : EgoConstraint, new()
{
    public EgoParentConstraint()
    {
        childConstraint = new CS1();
        childConstraint.parentConstraint = this;

        _mask[ComponentIDs.Get( typeof( C1 ) )] = true;
        _mask[ComponentIDs.Get( typeof( EgoComponent ) )] = true;

        // Attach built-in Event Handlers
        //EgoEvents<AddedGameObject>.AddHandler( Handle );
        //EgoEvents<DestroyedGameObject>.AddHandler( Handle );
        //EgoEvents<AddedComponent<C1>>.AddHandler( Handle );
        //EgoEvents<DestroyedComponent<C1>>.AddHandler( Handle );
    }

    protected override EgoBundle CreateBundle( EgoComponent egoComponent )
    {
        return new EgoBundle<C1>( egoComponent.GetComponent<C1>() );
    }

    #region Event Handlers
    void Handle( AddedComponent<C1> e )
    {
        
    }
    #endregion

    public delegate void ForEachGameObjectWithChildrentDelegate( EgoComponent egoComponent, C1 component1, CS1 childConstraint );

    public void ForEachGameObject( ForEachGameObjectWithChildrentDelegate callback )
    {
        var lookup = GetLookup( rootBundles );
        foreach( var kvp in lookup )
        {
            currentEgoComponent = kvp.Key;
            var bundle = kvp.Value as EgoBundle<C1>;
            callback( currentEgoComponent, bundle.component1, childConstraint as CS1 );
        }
    }
}
