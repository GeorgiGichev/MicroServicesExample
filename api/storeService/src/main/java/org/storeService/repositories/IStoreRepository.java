package org.storeService.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;
import org.storeService.models.Store;

@Repository
@Transactional
public interface IStoreRepository extends JpaRepository<Store, Integer> {

}
