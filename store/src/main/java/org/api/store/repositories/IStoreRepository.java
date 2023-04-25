package org.api.store.repositories;

import org.api.store.models.Store;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;


@Repository
@Transactional
public interface IStoreRepository extends JpaRepository<Store, Integer> {

}
